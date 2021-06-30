using System.Collections.Generic;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class CourseCollectionViewModel
    {
        private readonly CourseCollection coursesCollection;
        //public List<CourseViewModel> Courses { get; } = new List<CourseViewModel>();
        
        public CourseCollectionViewModel(CourseCollection coursesCollection)
        {
            this.coursesCollection = coursesCollection;
            //courses.ForEach(course => Courses.Add(new CourseViewModel(course)));
        }
    }
}