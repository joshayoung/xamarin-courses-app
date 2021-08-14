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
            set
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

        public Student(int id, string? name = null, int? age = null, string? major = null)
        {
            Id = id;
            Name = name ?? "";
            Age = age ?? 0;
            Major = major ?? "";
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}