using System.Collections.Generic;
using CoursesApp.Models.Service;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CoursesApp.Models.Test
{
    public class CourseTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            const string id = "1";
            const string title = "title";
            const float length = 2;
            var students = new List<int> { 1 };
            const CourseType type = CourseType.Lab;

            var course = new Course(id, title, length, type, students);

            course.Id.Should().Be(id);
            course.Title.Should().Be(title);
            course.Length.Should().Be(length);
            course.Type.Should().Be(type);
            course.Students.Should().BeEquivalentTo(students);
        }

        [Fact]
        public void Constructor_DefaultParams_ExpectAssignment()
        {
            const string id = "1";

            var course = new Course(id);

            course.Id.Should().Be(id);
            course.Title.Should().Be("");
            course.Length.Should().Be(0);
            course.Type.Should().Be(CourseType.None);
            course.Students.Should().BeEmpty();
        }


        [Fact]
        public void PropertiesChange_Called_ExpectPropertyChangedEvent()
        {
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
            course.Type = CourseType.Discussion;
            course.Students = new List<int> { 1 };
            wasTitleChanged.Should().BeTrue();
            wasLengthChanged.Should().BeTrue();
            wasTypeChanged.Should().BeTrue();
            wasStudentsChanged.Should().BeTrue();
        }

        [Fact]
        public void UpdateAverageAge_Called_PropertyChangeForAverageStudentAge()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService)
            {
                Students = { new Student(1) },
            };
            var course = new Course("1", "title", 2, CourseType.Discussion, new List<int>() { 1 });
            courseCollection.Courses.Add(course);
            var wasAverageUpdated = false;
            course.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Course.AverageStudentAge)) wasAverageUpdated = true;
            };

            course.UpdateAverageAge(courseCollection);

            wasAverageUpdated.Should().BeTrue();
        }

        [Fact]
        public void AverageStudentAge_Called_ExpectAverageAgeReturned()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            courseCollection.Students.Add(new Student(1, "Joe", 20, "Math"));
            courseCollection.Students.Add(new Student(2, "Sally", 30, "Math"));
            var students = new List<int>() { 1, 2 };
            var course = new Course("1", "title", 2, CourseType.Lab, students);

            course.UpdateAverageAge(courseCollection);
            var results = course.AverageStudentAge;

            results.Should().Be(25);
        }
        
        [Fact]
        public void AverageStudentAge_NoStudents_ExpectZeroReturned()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, null);

            course.UpdateAverageAge(courseCollection);
            var results = course.AverageStudentAge;

            results.Should().Be(0);
        }
    }
}