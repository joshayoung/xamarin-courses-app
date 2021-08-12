using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public sealed class StudentViewModel : INotifyPropertyChanged
    {
        private readonly Student student;
        private readonly CourseViewModel courseViewModel;
        
        public event PropertyChangedEventHandler? PropertyChanged;

        // TODO: Change this to return reasonable ages with the student's age selected
        public List<int> Ages => new List<int> { student.Age, 1, 2, 3 };

        public string? Name
        {
            get => student.Name;
            set
            {
                student.Name = value;
                NotifyPropertyChanged();
            }
        }

        public int Age
        {
            get => student.Age;
            set
            {
                student.Age = value;
                NotifyPropertyChanged();
            }
        }

        public string? Major
        {
            get => student.Major;
            set
            {
                student.Major = value;
                NotifyPropertyChanged();
            }
        }

        public StudentViewModel(Student student, CourseViewModel courseViewModel)
        {
            this.student = student;
            this.courseViewModel = courseViewModel;
            
            // Update the Model:
            student.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }

        public void AddStudent() => courseViewModel.AddStudent(student);

        public void DeleteStudent() => courseViewModel.DeleteStudent(student);

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}