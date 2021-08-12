using System;
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
            IEnumerable<CourseViewModel> courseList =
                courseCollection.Courses.Select(course => new CourseViewModel(course, courseCollection));
            Courses = new List<CourseViewModel>(courseList);
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