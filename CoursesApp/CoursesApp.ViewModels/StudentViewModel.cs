using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoursesApp.Models;
using CoursesApp.Models.Helpers;

namespace CoursesApp.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private readonly Student student;
        
        private readonly Course course;
        
        private readonly CourseCollection courseCollection;
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id => student.Id;

        public string Name
        {
            get => student.Name;
            set => student.Name = value;
        }

        public int Age
        {
            get => student.Age;
            set => student.Age = value;
        }
        
        public string Major
        {
            get => student.Major;
            set => student.Major = value;
        }
        
        public StudentViewModel(Student student, Course course, CourseCollection courseCollection)
        {
            this.student = student;
            this.course = course;
            this.courseCollection = courseCollection;
            
            // NOTE: If you update model value, update viewmodel value too
            student.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(Student.Name):
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                        break;
                    case nameof(Student.Age):
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Age)));
                        break;
                    case nameof(Student.Major):
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Major)));
                        break;
                }
            };
        }
        
        public static List<int> Ages => ModelHelper.StudentAges();

        public void AddStudent() => courseCollection.AddStudent(course, student);

        public void DeleteStudent() => courseCollection.DeleteStudent(course, student);

        public void SaveStudent(int id, StudentViewModel svm)
        {
            Student editedStudent = courseCollection.Students.First(s => s.Id == id);
            editedStudent.Name = svm.Name;
            editedStudent.Age = svm.Age;
            editedStudent.Major = svm.Major;
            course.UpdateAverageAge(courseCollection);
            course.UpdateOldestStudent(courseCollection);
        }

        public StudentViewModel EditStudentCopy()
        {
            var newStudent = new Student(student.Id, student.Name, student.Age, student.Major);
            
            return new StudentViewModel(newStudent, course, courseCollection);
        }
    }
}