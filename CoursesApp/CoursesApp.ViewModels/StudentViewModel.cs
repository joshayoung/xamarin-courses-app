using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CoursesApp.Models;
using CoursesApp.Models.Helpers;
using CoursesApp.Models.Service;

namespace CoursesApp.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private readonly Student student;
        private readonly Course course;
        private readonly CourseCollection courseCollection;
        private readonly List<int> ageList = ModelHelper.Ages();
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id => student.Id;

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

        // TODO: Save to API Here too
        public void SaveStudent(int id, StudentViewModel svm)
        {
            Student editedStudent = courseCollection.Students.First(s => s.Id == id);
            editedStudent.Name = svm.Name;
            editedStudent.Age = svm.Age;
            editedStudent.Major = svm.Major;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public StudentViewModel EditStudentCopy()
        {
            var newStudent = new Student(student.Id, student.Name, student.Age, student.Major);
            return new StudentViewModel(newStudent, course, courseCollection);
        }
    }
}