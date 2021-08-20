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
        
        // TODO: Move this to a helper method:
        private readonly List<int> ageList = new List<int>
        {
                17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30
        };
        
        public event PropertyChangedEventHandler? PropertyChanged;

        // TODO: Select the student's age
        public List<int> Ages
        {
            get
            {
                var myList = new List<int> { student.Age };
                myList.InsertRange(1, ageList);
                return myList;
            }
        }

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

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}