using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CoursesApp.Models
{
    public sealed class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id { get; }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        private int age;
        public int Age
        {
            get => age;
            set
            {
                age = value;
                NotifyPropertyChanged();
            }
        }

        private string major;
        public string Major
        {
            get => major;
            set
            {
                major = value;
                NotifyPropertyChanged();
            }
        }

        public Student(int id, string name = "", int age = 0, string major = "")
        {
            Id = id;
            this.name = name;
            this.age = age;
            this.major = major;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}