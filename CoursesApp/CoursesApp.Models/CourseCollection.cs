using System;
using System.Collections.Generic;
using System.ComponentModel;
using CoursesApp.Models.Service;

namespace CoursesApp.Models
{
    public class CourseCollection : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Course> Courses { get; private set; } = new List<Course>();

        public void RepopulateCourseList()
        {
            Courses.Clear();
            Courses = new CourseDataService().GetCourses();
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Courses)));
        }
    }
}