using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class CourseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly Course course;
        private readonly CourseCollection courseCollection;

        private int averageStudentAage;
        public int AverageStudentAage
        {
            get => GetAverageAge();
            private set
            {
                averageStudentAage = value;
                NotifyPropertyChanged(nameof(AverageStudentAage));
            }
        }

        private int numberOfStudents;
        public int NumberOfStudents
        {
            get => course.Students.Count;
            set
            {
                numberOfStudents = value;
                NotifyPropertyChanged(nameof(NumberOfStudents));
            }
        }

        // TODO: Refresh after adding a student
        public string CommonMajor
        {
            get
            {
                if (course.Students.Count < 1) return "";

                Dictionary<string, int> counts = new Dictionary<string, int>();
                foreach (var student in course.Students)
                {
                    if (!counts.ContainsKey(student.Major))
                    {
                        counts.Add(student.Major, 1);
                    }
                    else
                    {
                        counts[student.Major]++;
                    }
                }

                // TODO: Do not return the last value if all of the counts are the same
                return counts.OrderBy(v => v.Value).ToDictionary(v => v.Key, v => v.Value).Last().Key;
            }
        }

        public CourseType SelectedType
        {
            get => course.Type;
            set
            {
                course.Type = value;
                NotifyPropertyChanged(nameof(Type));
            }
        }

        public float SelectedLength
        {
            get => course.Length;
            set
            {
                course.Length = value;
                NotifyPropertyChanged(nameof(Length));
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
                NotifyPropertyChanged(nameof(Id));
            }
        }

        public string Title
        {
            get => course.Title;
            set
            {
                course.Title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        public float Length
        {
            get => course.Length;
            set
            {
                course.Length = value;
                NotifyPropertyChanged(nameof(Length));
            }
        }

        private List<StudentViewModel>? students;

        public List<StudentViewModel>? Students
        {
            get => students;
            set
            {
                students = value;
                NotifyPropertyChanged(nameof(Students));
            }
        }

        public CourseType Type
        {
            get => course.Type;
            set
            {
                course.Type = value;
                NotifyPropertyChanged(nameof(Type));
            }
        }

        public CourseViewModel(Course course, CourseCollection courseCollection)
        {
            this.course = course;
            this.courseCollection = courseCollection;
            RefreshStudents();
            course.PropertyChanged += OnPropertyChanged;
            course.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
            course.Students.ForEach(student => student.PropertyChanged += StudentOnPropertyChanged);
        }

        private void StudentOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Student.Age)) AverageStudentAage = GetAverageAge();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Students))
            {
                RefreshStudents();
                NumberOfStudents = course.Students.Count;
            }
        }

        private int GetAverageAge()
        {
            if (course.Students != null)
            {
                return course.Students.Sum(student => student.Age) / NumberOfStudents;
            }

            return 0;
        }

        private void RefreshStudents()
        {
            if (course.Students == null) return;

            IEnumerable<StudentViewModel>
                studentList = course.Students.Select(student => new StudentViewModel(student, this));
            Students = new List<StudentViewModel>(studentList);
        }

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void AddCourse() => courseCollection.AddCourse(course);

        public void EditCourse() => courseCollection.EditCourse(course);

        public void AddStudent(Student student) => course.AddStudent(student);
        
        public void DeleteStudent(Student student) => course.DeleteStudent(student);

        public StudentViewModel NewStudent() =>
            new StudentViewModel(new Student(), this);

        public void DeleteCourse() => courseCollection.DeleteCourse(course);
    }
}