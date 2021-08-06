using System.Collections.Generic;
using System.ComponentModel;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly Student student;
        private readonly CourseViewModel courseViewModel;

        public List<int> Ages
        {
            get => new List<int>()
            {
                student.Age,
                1,
                2,
                3
            };
        }

        public string? Name
        {
            get => student.Name;
            set
            {
                student.Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public int Age
        {
            get => student.Age;
            set
            {
                student.Age = value;
                NotifyPropertyChanged(nameof(Age));
            }
        }

        public string? Major
        {
            get => student.Major;
            set
            {
                student.Major = value;
                NotifyPropertyChanged(nameof(Major));
            }
        }

        public StudentViewModel(Student student, CourseViewModel courseViewModel)
        {
            this.student = student;
            this.courseViewModel = courseViewModel;
            student.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }

        private void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void AddStudent() => courseViewModel.AddStudent(student);

        public void DeleteStudent() => courseViewModel.DeleteStudent(student);
    }
}