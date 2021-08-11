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
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly Course course;
        private readonly CourseCollection courseCollection;

        private int averageStudentAge;
        public int AverageStudentAge
        {
            get => GetAverageAge();
            private set
            {
                averageStudentAge = value;
                NotifyPropertyChanged();
            }
        }

        private int numberOfStudents;
        public int NumberOfStudents
        {
            get => course.Students.Count;
        }

        public string OldestStudent
        {
            get
            {
            return course.Students.Count < 1 ? "" : course.Students.First(s => s.Age == students.Max(st => st.Age)).Name;
                
            }
        }
        
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

        public List<float> CourseLengthList
        {
            get => new List<float>
            {
                1, 2, 3, 4
            };
        }

        public List<CourseType> CourseTypesList
        {
            get => new List<CourseType>
            {
                CourseType.Seminar,
                CourseType.Lab,
                CourseType.Independent,
                CourseType.Lecture,
                CourseType.Discussion,
            };
        }

        public string Id
        {
            get => course.Id;
            set
            {
                course.Id = value;
                NotifyPropertyChanged();
            }
        }

        public string Title
        {
            get => course.Title;
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

        private List<StudentViewModel>? students;
        public List<StudentViewModel>? Students
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
            course.Students.ForEach(student => student.PropertyChanged += StudentOnPropertyChanged);
            
            course.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }

        private void StudentOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Student.Age))
            {
                AverageStudentAge = GetAverageAge();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AverageStudentAge)));
            }
        }

        private int GetAverageAge()
        {
            if (course.Students != null && NumberOfStudents != 0)
            {
                return course.Students.Sum(student => student.Age) / NumberOfStudents;
            }

            return 0;
        }

        private void RefreshStudents()
        {
            // if (course.Students == null) return;

            IEnumerable<StudentViewModel>
                studentList = course.Students.Select(student => new StudentViewModel(student, this));
            Students = new List<StudentViewModel>(studentList);
        }

        public void AddCourse() => courseCollection.AddCourse(course);

        public void EditCourse() => courseCollection.EditCourse(course);

        public void AddStudent(Student student) => course.AddStudent(student);

        public void DeleteStudent(Student student) => course.DeleteStudent(student);

        public StudentViewModel NewStudent() =>
            new StudentViewModel(new Student(), this);

        public void DeleteCourse() => courseCollection.DeleteCourse(course);

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Course.Students))
            {
                RefreshStudents();
            }
        }
    }
}