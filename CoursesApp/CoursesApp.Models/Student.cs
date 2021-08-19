using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CoursesApp.Models
{
    public sealed class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int id;
        public int Id
        {
            get => id;
            private set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        private string? name;
        public string? Name
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

        private string? major;
        public string? Major
        {
            get => major;
            set
            {
                major = value;
                NotifyPropertyChanged();
            }
        }

        public Student(int id, string? name = "", int? age = 0, string? major = "")
        {
            this.id = id;
            this.name = name;
            // TODO: Why do I need this here?
            this.age = age ?? 0;
            this.major = major;
        }

        // TODO: Is using '!' here a good idea:
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}