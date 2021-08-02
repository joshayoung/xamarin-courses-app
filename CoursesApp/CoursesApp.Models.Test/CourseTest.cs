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
            var title = "title";
            float length = 2;
            var students = new List<Student>();
            var type = CourseType.Lab;
            
            var course = new Course(title, length, type, students);

            course.Title.Should().Be(title);
            course.Length.Should().Be(length);
            course.Students.Should().BeEquivalentTo(students);
        }

        [Fact]
        public void Title_PropertyChanged_ExpectPropertyChangedEvent()
        {
            var title = "title";
            float length = 2;
            var students = new List<Student>();
            var type = CourseType.Lab;
            var wasChanged = false;
            
            var course = new Course(title, length, type, students);

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
            var title = "title";
            float length = 2;
            var students = new List<Student>();
            var type = CourseType.Lab;
            var wasChanged = false;
             
            var course = new Course(title, length, type, students);

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
            var title = "title";
            float length = 2;
            var students = new List<Student>();
            var type = CourseType.Lab;
            var wasChanged = false;
            
            var course = new Course(title, length, type, students);

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
        public void CourseType_PropertyChanged_ExpectPropertyChangedEvent()
        {
            var title = "title";
            float length = 2;
            var students = new List<Student>();
            var type = CourseType.Lab;
            var wasChanged = false;
            
            var course = new Course(title, length, type, students);

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