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

        public bool StudentsExist => Students.Count > 0;

        public float SelectedLength
        {
            get => course.Length;
            set
            {
                course.Length = value;
                NotifyPropertyChanged();
            }
        }

        public static List<float> CourseLengthList => new List<float> { 1, 2, 3, 4 };
        
        public int NumberOfStudents => course.Students.Count;

        public int AverageStudentAge
        {
            get
            {
                if (course.Students.Count == 0) return 0;
                
                var sum = 0;
                foreach (var id in course.Students)
                {
                    Student student = courseCollection.GetStudent(id);
                    sum += student.Age;
                }

                return sum / course.Students.Count;
            }
        }

        public string? OldestStudent {
            get
            {
                if (course.Students.Count == 0) return "";
                
                var age = 0;
                string? name = null;
                foreach (var student in course.Students.Select(id => courseCollection.GetStudent(id)).Where(student => student.Age > age))
                {
                    age = student.Age;
                    name = student.Name;
                }

                return name;
            }
        }

        public static List<CourseType> CourseTypesList =>
            new List<CourseType>
            {
                CourseType.Seminar,
                CourseType.Lab,
                CourseType.Independent,
                CourseType.Lecture,
                CourseType.Discussion,
            };

        public string Id => course.Id;

        public string? Title
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

        private List<StudentViewModel> students;

        public List<StudentViewModel> Students
        {
            get => students;
            set
            {
                students = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(StudentsExist));
                NotifyPropertyChanged(nameof(OldestStudent));
                NotifyPropertyChanged(nameof(NumberOfStudents));
                NotifyPropertyChanged(nameof(AverageStudentAge));
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
            students = new List<StudentViewModel>();
            this.course = course;
            this.courseCollection = courseCollection;
            RefreshStudents();
            courseCollection.PropertyChanged += OnPropertyChanged;

            course.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }

        private void RefreshStudents()
        {
            // Account for a new class:
            if (!courseCollection.Courses.Contains(course)) return;

            var cs = courseCollection.Courses.First(cs => cs == course);

            IEnumerable<StudentViewModel>
                studentList = cs.Students.Select(student =>
                    new StudentViewModel(courseCollection.GetStudent(student), cs, courseCollection));
            Students = new List<StudentViewModel>(studentList);
        }

        public void AddCourse() => courseCollection.AddCourse(course);

        public StudentViewModel NewStudent() =>
            new StudentViewModel(new Student(GetNextCourseId()), course, courseCollection);

        private int GetNextCourseId() => courseCollection.Students.Max(student => student.Id) + 1;

        public void DeleteCourse() => courseCollection.DeleteCourse(course);

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnPropertyChanged(object _, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CourseCollection.Courses)) RefreshStudents();
            if (e.PropertyName == nameof(CourseCollection.Students)) RefreshStudents();
        }
    }
}