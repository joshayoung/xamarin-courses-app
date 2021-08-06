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
            var student = new Student("name", 20, "Physics");
            var students = new List<Student> { student };
            var type = CourseType.Lab;

            var course = new Course(id, title, length, type, students);

            course.Id.Should().Be(id);
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

            var course = new Course("1", "title", 2, CourseType.Lab, new List<Student>());

            course.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(course.Id)) wasIdChanged = true;
                if (args.PropertyName == nameof(course.Title)) wasTitleChanged = true;
                if (args.PropertyName == nameof(course.Length)) wasLengthChanged = true;
                if (args.PropertyName == nameof(course.Students)) wasStudentsChanged = true;
                if (args.PropertyName == nameof(course.Type)) wasTypeChanged = true;
            };

            course.Id = "2";
            course.Title = "new title";
            course.Length = 4;
            course.Students = new List<Student> { new Student("name", 20, "Physics") };
            course.Type = CourseType.Discussion;
            wasIdChanged.Should().BeTrue();
            wasTitleChanged.Should().BeTrue();
            wasLengthChanged.Should().BeTrue();
            wasStudentsChanged.Should().BeTrue();
            wasTypeChanged.Should().BeTrue();
        }

        [Fact]
        public void AddStudent_Called_ExpectStudentsUpdated()
        {
            var student = new Student("name", 20, "Physics");
            var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { student });
            
            course.AddStudent(student);

            course.Students.Count.Should().Be(2);
        }

        [Fact]
        public void AddStudent_Called_ExpectPropertyChangedEvent()
        {
            var wasStudentsChanged = false;
            var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { });
            course.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(course.Students)) wasStudentsChanged = true;
            };
            
            course.AddStudent(new Student("name", 20, "Physics"));

            wasStudentsChanged.Should().BeTrue();
        }
        
        [Fact]
        public void DeleteStudent_Called_ExpectStudentsUpdated()
        {
            var student = new Student("name", 20, "Physics");
            var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { student });
            
            course.DeleteStudent(student);

            course.Students.Count.Should().Be(0);
        }
        
        [Fact]
        public void DeleteStudent_Called_ExpectPropertyChangedEvent()
        {
            var wasStudentsChanged = false;
            var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { });
            course.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(course.Students)) wasStudentsChanged = true;
            };
            
            course.DeleteStudent(new Student("name", 20, "Physics"));

            wasStudentsChanged.Should().BeTrue();
        }
    }
}