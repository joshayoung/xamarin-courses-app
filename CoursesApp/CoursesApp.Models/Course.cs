using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CoursesApp.Models
{
    public class Course : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public int Id { get; }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                // NOTE: Refresh property when changed:
                NotifyPropertyChanged();
            }
        }

        private float length;
        public float Length
        {
            get => length;
            set
            {
                length = value;
                // NOTE: Refresh property when changed:
                NotifyPropertyChanged();
            }
        }

        private CourseType type;
        public CourseType Type
        {
            get => type;
            set
            {
                type = value;
                // NOTE: Refresh property when changed:
                NotifyPropertyChanged();
            }
        }

        public List<int> Students { get; set; }

        public int AverageStudentAge { get; private set; }

        public string OldestStudent { get; private set; } = "";

        public int NumberOfStudents { get; private set; }
        
        public bool StudentsExist { get; private set; }

        public Course(
            int id,
            string title = "",
            float length = 0,
            CourseType type = CourseType.None,
            List<int>? students = null)
        {
            Id = id;
            this.title = title;
            this.length = length;
            this.type = type;
            this.Students = students ?? new List<int>();
        }
        
        public virtual void UpdateAverageAge(CourseCollection courseCollection)
        {
            if (Students.Count == 0)
            {
                AverageStudentAge = 0;
                // Note: Trigger a property change after update:
                NotifyPropertyChanged(nameof(AverageStudentAge));
                return;
            }
            AverageStudentAge = Students
                .Select(courseCollection.GetStudent)
                .Select(student => student.Age)
                .Sum() / Students.Count;

            // Note: Trigger a property change after update:
            NotifyPropertyChanged(nameof(AverageStudentAge));
        }

        public void UpdateStudentCount()
        {
            NumberOfStudents = Students.Count;
            NotifyPropertyChanged(nameof(NumberOfStudents));
        }
        
        public void UpdateOldestStudent(CourseCollection courseCollection)
        {
            var age = 0;
            string name = "";
            
            if (Students.Count == 0) name = "";

            foreach (var student in Students.Select(courseCollection.GetStudent)
                .Where(student => student.Age > age))
            {
                age = student.Age;
                name = student.Name;
            }

            OldestStudent = name;
            // Note: Trigger a property change after update:
            NotifyPropertyChanged(nameof(OldestStudent));
        }

        public void UpdateStudentsExist()
        {
            StudentsExist = Students.Count > 0;
            // Note: Trigger a property change after update:
            NotifyPropertyChanged(nameof(StudentsExist));
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}