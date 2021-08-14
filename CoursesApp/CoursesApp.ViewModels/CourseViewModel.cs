using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class CourseViewModel : INotifyPropertyChanged
    {
        private readonly Course course;
        private readonly CourseCollection courseCollection;
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public CourseType SelectedType
        {
            get => course.Type;
            set
            {
                course.Type = value;
                NotifyPropertyChanged();
            }
        }

        public float SelectedLength
        {
            get => course.Length;
            set
            {
                course.Length = value;
                NotifyPropertyChanged();
            }
        }

        public List<float> CourseLengthList => new List<float> { 1, 2, 3, 4 };

        public List<CourseType> CourseTypesList =>
            new List<CourseType>
            {
                CourseType.Seminar,
                CourseType.Lab,
                CourseType.Independent,
                CourseType.Lecture,
                CourseType.Discussion,
            };

        public string Id
        {
            get => course.Id;
            set
            {
                course.Id = value;
                NotifyPropertyChanged();
            }
        }

        public string? Title
        {
            get => course?.Title;
            set
            {
                course.Title = value;
                NotifyPropertyChanged();
            }
        }

        public float Length
        {
            get => course.Length;
            set
            {
                course.Length = value;
                NotifyPropertyChanged();
            }
        }

        private List<StudentViewModel> students;
        public List<StudentViewModel> Students
        {
            get => students;
            set
            {
                students = value;
                NotifyPropertyChanged();
            }
        }

        public CourseType Type
        {
            get => course.Type;
            set
            {
                course.Type = value;
                NotifyPropertyChanged();
            }
        }

        public CourseViewModel(Course course, CourseCollection courseCollection)
        {
            this.course = course;
            this.courseCollection = courseCollection;
            RefreshStudents();
            course.PropertyChanged += OnPropertyChanged;
            // course.Students?.ForEach(student => student.PropertyChanged += StudentOnPropertyChanged);
            
            course.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }

        private void StudentOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Student.Age)) return;
            // AverageStudentAge = GetAverageAge();
            // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AverageStudentAge)));
        }

        private void RefreshStudents()
        {
            Course cs = courseCollection.Courses.Last();

            IEnumerable<StudentViewModel>
                studentList = cs.Students.Select(student => new StudentViewModel(GetStudent(student), this));
            Students = new List<StudentViewModel>(studentList);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Students)));
            Console.WriteLine("Looks like it is updating Students, but not the page?");
        }

        private Student GetStudent(int id)
        {
            return courseCollection.Students.Find(student => student.Id == id);
        }

        public void AddCourse() => courseCollection.AddCourse(course);

        public void EditCourse() => courseCollection.EditCourse(course);

        public void AddStudent(Student student) => courseCollection.AddStudent(course, student);

        public StudentViewModel NewStudent() => new StudentViewModel(new Student(GetNextCourseId()), this);
        
        private int GetNextCourseId()
        {
            if (course.Students.Count == 0) return 1;
            
            int maxLength = course.Students.Max();
            Console.WriteLine("test");
            return maxLength + 1;
        }

        public void DeleteCourse() => courseCollection.DeleteCourse(course);

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(course.Students))
            {
                RefreshStudents();
            }
        }
    }
}