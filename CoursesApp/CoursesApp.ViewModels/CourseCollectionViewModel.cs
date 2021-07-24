using System.Collections.Generic;
using System.ComponentModel;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class CourseCollectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<CourseViewModel> Courses { get; } = new List<CourseViewModel>();
        
        public CourseCollectionViewModel(CourseCollection coursesCollection)
        {
            coursesCollection.Courses.ForEach(course => Courses.Add(new CourseViewModel(course, coursesCollection)));
        }
    }
}