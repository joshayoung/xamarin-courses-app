using System.Collections.ObjectModel;
using System.ComponentModel;
using CoursesApp.Models.Builders;

namespace CoursesApp.Models
{
    public class CourseCollection : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public readonly ObservableCollection<Course> Courses = new ObservableCollection<Course>();

        public void RepopulateCourseList()
        {
            Courses.Clear();
            var allCourses = CourseBuilder.Build();
            allCourses.ForEach(cs => Courses.Add(cs));
        }
    }
}