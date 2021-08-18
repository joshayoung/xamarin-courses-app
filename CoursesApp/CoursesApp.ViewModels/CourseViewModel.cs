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
            courseCollection.PropertyChanged += OnPropertyChanged;

            course.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }

        private void RefreshStudents()
        {
            // Account for a new class:
            if (!courseCollection.Courses.Contains(course)) return;

            var cs = courseCollection.Courses.First(cs => cs == course);

            IEnumerable<StudentViewModel>
                studentList = cs.Students.Select(student => new StudentViewModel(GetStudent(student), cs, courseCollection));
            Students = new List<StudentViewModel>(studentList);
        }

        private Student GetStudent(int id) => courseCollection.Students.Find(student => student.Id == id);

        public void AddCourse() => courseCollection.AddCourse(course);

        public void EditCourse() => courseCollection.EditCourse(course);

        public StudentViewModel NewStudent() => new StudentViewModel(new Student(GetNextCourseId()), course, courseCollection);

        private int GetNextCourseId() => courseCollection.Students.Max(student => student.Id) + 1;

        public void DeleteCourse() => courseCollection.DeleteCourse(course);

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
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