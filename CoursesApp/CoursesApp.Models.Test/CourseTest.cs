using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace CoursesApp.Models.Test
{
    public class CourseTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            string title = "title";
            float length = 2;
            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();
            CourseType type = CourseType.Lab;
            
            var course = new Course(title, length, type);

            course.Title.Should().Be(title);
        }
    }
}