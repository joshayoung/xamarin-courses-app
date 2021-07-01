using System.Collections.ObjectModel;
using System.ComponentModel;
using CoursesApp.Models.Service;

namespace CoursesApp.Models
{
    public class CourseCollection : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ObservableCollection<Course> Courses { get; } = new ObservableCollection<Course>();

        public void RepopulateCourseList()
        {
            var allCourses = CourseDataService.GetCourses();
            
            Courses.Clear();
            allCourses.ForEach(cs => Courses.Add(cs));
        }
    }
}