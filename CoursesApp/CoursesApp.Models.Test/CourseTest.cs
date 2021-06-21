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

        [Fact]
        public void Title_PropertyChanged_ExpectPropertyChangedEvent()
        {
            string title = "title";
            float length = 2;
            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();
            CourseType type = CourseType.Lab;
            bool wasChanged = false;
            
            var course = new Course(title, length, type);

            course.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(course.Title))
                {
                    wasChanged = true;
                }
            };

            course.Title = "new title";
            wasChanged.Should().BeTrue();
        }
        
        [Fact]
        public void Length_PropertyChanged_ExpectPropertyChangedEvent()
        {
            string title = "title";
            float length = 2;
            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();
            CourseType type = CourseType.Lab;
            bool wasChanged = false;
            
            var course = new Course(title, length, type);

            course.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(course.Length))
                {
                    wasChanged = true;
                }
            };

            course.Length = 4;
            wasChanged.Should().BeTrue();
        }
        
        [Fact]
        public void Students_PropertyChanged_ExpectPropertyChangedEvent()
        {
            string title = "title";
            float length = 2;
            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();
            CourseType type = CourseType.Lab;
            bool wasChanged = false;
            
            var course = new Course(title, length, type);

            course.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(course.Students))
                {
                    wasChanged = true;
                }
            };

            course.Students = new List<Student>();
            wasChanged.Should().BeTrue();
        }
        
        [Fact]
        public void Teachers_PropertyChanged_ExpectPropertyChangedEvent()
        {
            string title = "title";
            float length = 2;
            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();
            CourseType type = CourseType.Lab;
            bool wasChanged = false;
            
            var course = new Course(title, length, type);

            course.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(course.Teachers))
                {
                    wasChanged = true;
                }
            };

            course.Teachers = new List<Teacher>();
            wasChanged.Should().BeTrue();
        }
        
        [Fact]
        public void CourseType_PropertyChanged_ExpectPropertyChangedEvent()
        {
            string title = "title";
            float length = 2;
            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();
            CourseType type = CourseType.Lab;
            bool wasChanged = false;
            
            var course = new Course(title, length, type);

            course.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(course.Type))
                {
                    wasChanged = true;
                }
            };

            course.Type = CourseType.Discussion;
            wasChanged.Should().BeTrue();
        }
    }
}