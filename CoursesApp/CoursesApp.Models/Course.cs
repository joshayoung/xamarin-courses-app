using System;
using System.Collections.Generic;

namespace CoursesApp.Models
{
    public class Course
    {
        public string Title;
        public float Length;
        public List<Student> Students = new List<Student>();
        public List<Teacher> Teachers = new List<Teacher>();
        public CourseType Type;

        public Course(string title, float length, CourseType type)
        {
            Title = title;
            Length = length;
            Type = type;
        }
    }
}