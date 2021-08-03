using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoursesApp.Models.Service;

namespace CoursesApp.Models
{
    public class CourseCollection : INotifyPropertyChanged
    {
        private readonly CourseDataService courseDataService;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public List<Course> Courses { get; private set; } = new List<Course>();

        public CourseCollection(CourseDataService courseDataService)
        {
            this.courseDataService = courseDataService;
        }

        public void RepopulateCourseList()
        {
            Courses = courseDataService.GetCourses();
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Courses)));
        }

        public void EditCourse(Course course)
        {
            // This is where I could call out to my API to save this record in the DB.
        }
    }
}