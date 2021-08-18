using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public sealed class CourseCollectionViewModel : INotifyPropertyChanged
    {
        private readonly CourseCollection courseCollection;

        public event PropertyChangedEventHandler? PropertyChanged;

        private List<CourseViewModel>? courses;

        public List<CourseViewModel>? Courses
        {
            get => courses;
            set
            {
                courses = value;
                // Needed to repopulate the course list after adding/removing a course:
                OnPropertyChanged();
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

        // TODO: Clean this up:
        private void RefreshList()
        {
            var courseList = new List<CourseViewModel>();
            foreach (var course in courseCollection.Courses)
            {
                var vm = new CourseViewModel(course, courseCollection);
                var studentList = new List<StudentViewModel>();
                foreach (var id in course.Students)
                {
                    var student = courseCollection.Students.First(student => student.Id == id);
                    studentList.Add(new StudentViewModel(student, course, courseCollection));
                }

                Students = studentList;
                vm.Students = new List<StudentViewModel>(studentList);
                courseList.Add(vm);
            }

            Courses = courseList;
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

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}