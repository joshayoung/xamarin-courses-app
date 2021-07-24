using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class CourseCollectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CourseViewModel LastCourse => FindLastCourse();

        public List<CourseViewModel> Courses { get; } =
            new List<CourseViewModel>();
        
        public readonly CourseCollection CoursesCollection;

        public CourseCollectionViewModel(CourseCollection coursesCollection)
        {
            CoursesCollection = coursesCollection;
            CoursesCollection.Courses.ForEach(course => Courses.Add(new CourseViewModel(course)));
            
            // coursesCollection.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }

        private CourseViewModel FindLastCourse() =>
            (CoursesCollection.Courses.Count > 0) ? new CourseViewModel(CoursesCollection.Courses.Last()) : null;
        
        public void ReloadTheClasses() => CoursesCollection.RepopulateCourseList();
    }
}