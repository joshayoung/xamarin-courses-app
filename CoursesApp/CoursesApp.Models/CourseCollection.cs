using System.Collections.Generic;
using System.ComponentModel;
using CoursesApp.Models.Service;

namespace CoursesApp.Models
{
    public class CourseCollection : INotifyPropertyChanged
    {
        private readonly CourseDataService courseDataService;
        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual List<Course> Courses { get; private set; } = new List<Course>();

        public CourseCollection(CourseDataService courseDataService)
        {
            this.courseDataService = courseDataService;
        }

        public void RepopulateCourseList()
        {
            Courses = courseDataService.GetCourses();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Courses)));
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

        public void DeleteCourse(Course course)
        {
            Courses.Remove(course);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Courses)));
        }
    }
}