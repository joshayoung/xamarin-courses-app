using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CoursesApp.Models;
using Xamarin.Forms.Internals;

namespace CoursesApp.ViewModels
{
    public sealed class CourseCollectionViewModel : INotifyPropertyChanged
    {
        private readonly CourseCollection courseCollection;
        
        private List<CourseViewModel>? courses;
        public event PropertyChangedEventHandler? PropertyChanged;

        public List<CourseViewModel>? Courses
        {
            get => courses;
            set
            {
                courses = value;
                OnPropertyChanged();
            }
        }
        
        private List<StudentViewModel>? students;
        public List<StudentViewModel>? Students
        {
            get => students;
            set
            {
                students = value;
                OnPropertyChanged();
            }
        }

        public CourseCollectionViewModel(CourseCollection? courseCollection)
        {
            this.courseCollection = courseCollection ?? throw new ArgumentException();

            courseCollection.PropertyChanged += CoursesCollectionOnPropertyChanged;
            RefreshList();
        }

        private void CoursesCollectionOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CourseCollection.Courses)) RefreshList();
        }

        private void RefreshList()
        {

            // foreach (var course in courseCollection.Courses)
            // {
            //     var vm = new CourseViewModel(course, courseCollection);
            //     var studentVmList = new List<StudentViewModel>();
            //     foreach (var id in course.Students)
            //     {
            //         var student = courseCollection.Students.First(student => student.Id == id);
            //         // studentVmList.Add(student);
            //         Console.WriteLine("test");
            //     }
            //     // vm.Students = new
            //
            // }


            // IEnumerable<CourseViewModel> courseList =
            //     courseCollection.Courses.Select(course => new CourseViewModel(course, courseCollection));
            // Courses = new List<CourseViewModel>(courseList);

            Courses = new List<CourseViewModel>();
            
            foreach (var course in courseCollection.Courses)
            {
                var studentIds = course.Students;
                var studentList = new List<StudentViewModel>();

                foreach (var id in studentIds)
                {
                    var student = courseCollection.Students.First(student => student.Id == id);
                    studentList.Add(new StudentViewModel(student));
                }

                var vm = new CourseViewModel(course, courseCollection);
                vm.Students = studentList;
                Courses.Add(vm);
            }

            // IEnumerable<StudentViewModel> studentList =
            //     courseCollection.Students.Select(student => new StudentViewModel(student));
            // Students = new List<StudentViewModel>(studentList);
            //
            // IEnumerable<CourseViewModel> courseList =
            //     courseCollection.Courses.Select(course => new CourseViewModel(course, courseCollection));
            // Courses = new List<CourseViewModel>(courseList);
            //
            // foreach (var courseVm in Courses)
            // {
            //     courseVm.Students = new List<StudentViewModel>();
            //
            // }
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