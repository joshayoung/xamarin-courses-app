using System.Collections.Generic;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class CourseCollectionViewModel
    {
        public List<CourseViewModel> Courses { get; } = new List<CourseViewModel>();
        public CourseCollectionViewModel(List<Course> courses)
        {
            courses.ForEach(course => Courses.Add(new CourseViewModel(course)));
        }
    }
}