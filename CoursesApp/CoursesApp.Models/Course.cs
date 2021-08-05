using System.Collections.Generic;
using System.ComponentModel;
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
                NotifyPropertyChanged(nameof(Id));
            }
        }
        
        private string? title;
        public string? Title
        {
            get => title;
            set
            {
                title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        private float length;
        public float Length
        {
            get => length;
            set
            {
                length = value;
                NotifyPropertyChanged(nameof(Length));
            }
        }

        private List<Student>? students;
        public List<Student>? Students
        {
            get => students;
            set
            {
                students = value;
                NotifyPropertyChanged(nameof(Students));
            }
        }

        private CourseType type;
        public CourseType Type
        {
            get => type;
            set
            {
                type = value;
                NotifyPropertyChanged(nameof(Type));
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

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
    }
}