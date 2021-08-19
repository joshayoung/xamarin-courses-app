using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using CoursesApp.Models;
using Xamarin.Forms;

namespace CoursesApp.ViewModels
{
    public sealed class CourseCollectionViewModel : INotifyPropertyChanged
    {
        private readonly CourseCollection courseCollection;

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        private bool coursesExist;
        public bool CoursesExist
        {
            get { return (Courses?.Count > 0); }
            set
            {
                coursesExist = value;
                OnPropertyChanged();
            }
        }

        private List<CourseViewModel>? courses;

        public List<CourseViewModel>? Courses
        {
            get => courses;
            set
            {
                courses = value;
                // Needed to repopulate the course list after adding/removing a course:
                OnPropertyChanged();
                OnPropertyChanged(nameof(CoursesExist));
            }
        }

        private List<StudentViewModel>? students;

        public List<StudentViewModel>? Students
        {
            get => students;
            private set
            {
                students = value;
                OnPropertyChanged();
            }
        }

        public CourseCollectionViewModel(CourseCollection courseCollection)
        {
            this.courseCollection = courseCollection;
            courseCollection.PropertyChanged += CoursesCollectionOnPropertyChanged;
            RefreshList();
        }

        private void CoursesCollectionOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CourseCollection.Courses)) RefreshList();
            if (e.PropertyName == nameof(CourseCollection.Students)) RefreshList();
        }

        private void RefreshList()
        {
            var courseList = new List<CourseViewModel>();
            var studentList = new List<StudentViewModel>();
            courseCollection.Courses.ForEach(cs =>
            {
                studentList = cs.Students.Select(id => StudentVm(id, cs)).ToList();
                courseList.Add(CourseVm(cs, studentList));
            });
            Students = studentList;
            Courses = courseList;
        }

        private CourseViewModel? CourseVm(Course cs, IEnumerable<StudentViewModel> studentList)
        {
            var courseVm = new CourseViewModel(cs, courseCollection)
            {
                Students = new List<StudentViewModel>(studentList)
            };
            return courseVm;
        }

        private StudentViewModel StudentVm(int id, Course? cs)
        {
            return new StudentViewModel(courseCollection.GetStudent(id), cs, courseCollection);
        }

        public CourseViewModel NewCourseViewModel()
        {
            return new CourseViewModel(new Course(GetNextCourseId().ToString()), courseCollection);
        }

        private int GetNextCourseId()
        {
            if (Courses == null || Courses.Count == 0) return 1;

            var id = int.Parse(Courses.Max(course => course.Id));

            return ++id;
        }

        public void Refresh()
        {
            IsRefreshing = true;
            courseCollection.RepopulateCourseList();
            IsRefreshing = false;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}