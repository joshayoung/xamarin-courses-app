using System.ComponentModel;

namespace CoursesApp.Models
{
    public class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
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

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        
        private int age;
        public int Age
        {
            get => age;
            set
            {
                age = value;
                NotifyPropertyChanged(nameof(Age));
            }
        }
        
        private string major;
        public string Major
        {
            get => major;
            set
            {
                major = value;
                NotifyPropertyChanged(nameof(Major));
            }
        }

        public Student(string name, int age, string major)
        {
            Name = name;
            Age = age;
            Major = major;
        }

        private void NotifyPropertyChanged(string theName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(theName));
        }
    }
}