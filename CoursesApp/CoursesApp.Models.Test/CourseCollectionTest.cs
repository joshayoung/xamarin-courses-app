using System.Collections.Generic;
using CoursesApp.Models.Service;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.Extensions;
using Xunit;

namespace CoursesApp.Models.Test
{
    public class CourseCollectionTest
    {
        private const string Json = "[{\"id\": 4, \"title\": \"Physical Science\", \"length\": 4, \"type\": 3, \"students\": []}]";
        private readonly List<Course> deserializedCourse;

        public CourseCollectionTest()
        {
            deserializedCourse = JsonConvert.DeserializeObject<List<Course>>(Json);
        }

        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);

            courseCollection.Courses.Should().BeEmpty();
        }

        [Fact]
        public void RepopulateCourseList_Called_ExpectAPropertyChangedEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            courseDataService.Configure().GetCourses().Returns(deserializedCourse);
            var courseCollection = new CourseCollection(courseDataService);
            var coursesWasChanged = false;
            courseCollection.PropertyChanged += (_, __) => coursesWasChanged = true;
            
            courseCollection.RepopulateCourseList();

            coursesWasChanged.Should().BeTrue();
        }

        [Fact]
        public void RepopulateCourseList_Called_ExpectCoursesToChange()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            courseDataService.Configure().GetCourses().Returns(deserializedCourse);
            var courseCollection = new CourseCollection(courseDataService);
            
            courseCollection.RepopulateCourseList();

            courseCollection.Courses.Should().BeEquivalentTo(deserializedCourse);
        }

        [Fact]
        public void AddCourse_Called_ExpectANewCourseAddedToList()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = Substitute.ForPartsOf<Course>("id", "title", 1, CourseType.Discussion, new List<Student>(0));
            
            courseCollection.AddCourse(course);
            
            courseCollection.Courses.ForEach(c => ValidateCourse(course, c));
        }
        
        [Fact]
        public void AddCourse_Called_ExpectCoursesToChange()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = Substitute.ForPartsOf<Course>("id", "title", 1, CourseType.Discussion, new List<Student>(0));
            var coursesWasChanged = false;
            courseCollection.PropertyChanged += (_, __) => coursesWasChanged = true;
            
            courseCollection.AddCourse(course);

            coursesWasChanged.Should().BeTrue();
        }
        
        // TODO: Add `EditCourse` test once you add a method body

        [Fact]
        public void DeleteCourse_Called_ExpectACourseToBeRemoved()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = Substitute.ForPartsOf<Course>("id", "title", 1, CourseType.Discussion, new List<Student>(0));
            courseCollection.Courses.Add(course);
            
            courseCollection.DeleteCourse(course);

            courseCollection.Courses.Should().BeEmpty();
        }
        
        [Fact]
        public void DeleteCourse_Called_ExpectCoursesToChange()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = Substitute.ForPartsOf<Course>("id", "title", 1, CourseType.Discussion, new List<Student>(0));
            var coursesWasChanged = false;
            courseCollection.PropertyChanged += (_, __) => coursesWasChanged = true;
            
            courseCollection.DeleteCourse(course);

            coursesWasChanged.Should().BeTrue();
        }

        private void ValidateCourse(Course course, Course newCourse)
        {
            course.Id.Should().Be(newCourse.Id);
            course.Title.Should().Be(newCourse.Title);
            course.Length.Should().Be(newCourse.Length);
            course.Students.Should().BeEquivalentTo(newCourse.Students);
            course.Type.Should().Be(newCourse.Type);
        }
    }
}