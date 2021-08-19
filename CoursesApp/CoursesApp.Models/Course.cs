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
            // TODO: Is this value getting used?
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

        private List<int> students;
        public List<int> Students
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
            List<int>? students = null)
        {
            // TODO: The best way to do this?
            this.id = id;
            this.title = title ?? "";
            this.length = length;
            this.students = students ?? new List<int>();
            this.type = type;
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}