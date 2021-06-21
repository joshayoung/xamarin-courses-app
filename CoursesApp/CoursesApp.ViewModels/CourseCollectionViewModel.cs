﻿using System.Collections.Generic;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class CourseCollectionViewModel
    {
        public List<CourseViewModel> AllCourses => new List<CourseViewModel>();
        public CourseCollectionViewModel(List<Course> courses)
        {
            courses.ForEach(course => AllCourses.Add(new CourseViewModel(course)));
        }
    }
}