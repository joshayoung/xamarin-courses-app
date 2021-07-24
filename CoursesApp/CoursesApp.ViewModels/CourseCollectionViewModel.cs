using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class CourseCollectionViewModel : INotifyPropertyChanged
    {
        private readonly CourseCollection coursesCollection;
        private List<CourseViewModel> courses;

        public List<CourseViewModel> Courses
        {
            get => courses;
            set
            {
                courses = value;
                OnPropertyChanged();
            }
        }

        public CourseCollectionViewModel(CourseCollection coursesCollection)
        {
            this.coursesCollection = coursesCollection;
            
            coursesCollection.PropertyChanged += CoursesCollectionOnPropertyChanged;
            
            RefreshList();
        }

        private void CoursesCollectionOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CourseCollection.Courses))
            {
                RefreshList();
            }
        }

        private void RefreshList()
        {
            IEnumerable<CourseViewModel> courseList =
                coursesCollection.Courses.Select(course => new CourseViewModel(course, coursesCollection));
            Courses = new List<CourseViewModel>(courseList);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}