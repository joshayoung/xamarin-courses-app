using System.ComponentModel;

namespace CoursesApp.Models
{
    public class Teacher : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        private int experience;
        public int Experience
        {
            get => experience;
            set
            {
                experience = value;
                NotifyPropertyChanged(nameof(Experience));
            }
        }

        public Teacher(string name, int age, int experience)
        {
            Name = name;
            Age = age;
            Experience = experience;
        }

        private void NotifyPropertyChanged(string theName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(theName));
        }
    }
}