using System.ComponentModel;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class TeacherViewModel : INotifyPropertyChanged
    {
        private readonly Teacher teacher;

        public event PropertyChangedEventHandler PropertyChanged;
        
        public string Name
        {
            get => teacher.Name;
            set
            {
                teacher.Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public int Age
        {
            get => teacher.Age;
            set
            {
                teacher.Age = value;
                NotifyPropertyChanged(nameof(Age));
            }
        }

        public int Experience
        {
            get => teacher.Experience;
            set
            {
                teacher.Experience = value;
                NotifyPropertyChanged(nameof(Experience));
            }
        }

        public TeacherViewModel(Teacher teacher)
        {
            this.teacher = teacher;
            
            // Update the model:
            teacher.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }
        
        private void NotifyPropertyChanged(string theName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(theName));
        }
    }
}