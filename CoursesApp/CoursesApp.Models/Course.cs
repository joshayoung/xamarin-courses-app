using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace CoursesApp.Models
{
    public class Course : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string id;
        public string Id
        {
            get => id;
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        private string? title;
        public string? Title
        {
            get => title;
            set
            {
                title = value;
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
                NotifyPropertyChanged();
            }
        }

        private List<Student>? students;
        public List<Student>? Students
        {
            get => students;
            set
            {
                students = value;
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
                NotifyPropertyChanged();
            }
        }

        [JsonConstructor]
        public Course(
            string id,
            string title = "",
            float length = 0,
            CourseType type = CourseType.Lecture,
            List<Student>? students = null)
        {
            this.id = id;
            this.title = title ?? "";
            this.length = length;
            this.students = students ?? new List<Student>();
            this.type = type;
        }

        public void AddStudent(Student student)
        {
            students?.Add(student);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Students)));
        }

        public void DeleteStudent(Student student)
        {
            students?.Remove(student);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Students)));
        }


        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}