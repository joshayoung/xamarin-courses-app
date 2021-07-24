using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace CoursesApp.Models
{
    public class Course : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private string title;
        public string Title
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

        private List<Student> students;
        public List<Student> Students
        {
            get => students;
            set
            {
                students = value;
                NotifyPropertyChanged(nameof(Students));
            }
        }

        private List<Teacher> teachers;
        public List<Teacher> Teachers
        {
            get => teachers;
            set
            {
                teachers = value;
                NotifyPropertyChanged(nameof(Teachers));
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
            string title, 
            float length, 
            List<Student> students, 
            List<Teacher> teachers, 
            CourseType type)
        {
            Title = title;
            Length = length;
            Students = students;
            Teachers = teachers;
            Type = type;
        }

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}