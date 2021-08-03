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
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Students)) RefreshStudents();
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

        public StudentViewModel NewStudent() =>
            new StudentViewModel(new Student(), this);
    }
}