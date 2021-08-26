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
            var id = "1";
            var title = "title";
            float length = 2;
            var students = new List<int> { 1 };
            var type = CourseType.Lab;
        
            var course = new Course(id, title, length, type, students);
        
            course.Title.Should().Be(title);
            course.Length.Should().Be(length);
            course.Students.Should().BeEquivalentTo(students);
            course.Type.Should().Be(type);
        }

        [Fact]
        public void PropertiesChange_Called_ExpectPropertyChangedEvent()
        {
            var wasIdChanged = false;
            var wasTitleChanged = false;
            var wasLengthChanged = false;
            var wasStudentsChanged = false;
            var wasTypeChanged = false;
        
            var course = new Course("1", "title", 2, CourseType.Lab, new List<int>());
        
            course.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(course.Title)) wasTitleChanged = true;
                if (args.PropertyName == nameof(course.Length)) wasLengthChanged = true;
                if (args.PropertyName == nameof(course.Students)) wasStudentsChanged = true;
                if (args.PropertyName == nameof(course.Type)) wasTypeChanged = true;
            };
        
            course.Title = "new title";
            course.Length = 4;
            course.Students = new List<int> { 1 };
            course.Type = CourseType.Discussion;
            wasIdChanged.Should().BeTrue();
            wasTitleChanged.Should().BeTrue();
            wasLengthChanged.Should().BeTrue();
            wasStudentsChanged.Should().BeTrue();
            wasTypeChanged.Should().BeTrue();
        }
    }
}