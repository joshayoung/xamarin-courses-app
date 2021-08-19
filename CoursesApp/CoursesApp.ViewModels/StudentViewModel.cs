using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private readonly Student student;
        private readonly Course course;
        private readonly CourseCollection courseCollection;
        
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

        public StudentViewModel(Student student, Course course, CourseCollection courseCollection)
        {
            this.student = student;
            this.course = course;
            this.courseCollection = courseCollection;
            
            student.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }

        public void AddStudent() => courseCollection.AddStudent(course, student);
        public void DeleteStudent() => courseCollection.DeleteStudent(course, student);
        public void EditStudent() => courseCollection.EditStudent(course, student);

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}