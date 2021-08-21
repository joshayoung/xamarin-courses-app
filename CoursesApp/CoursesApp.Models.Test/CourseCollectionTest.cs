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
        private const string JsonC = "[{\"id\": 4, \"title\": \"Physical Science\", \"length\": 4, \"type\": 3, \"students\": []}]";
        private const string JsonS = "[{\"id\":2,\"name\":\"Sarah\",\"age\":18,\"major\":\"History\"}]";
        private readonly List<Course> deserializedCourses;
        private readonly List<Student> deserializedStudents;

        public CourseCollectionTest()
        {
            deserializedCourses = JsonConvert.DeserializeObject<List<Course>>(JsonC);
            deserializedStudents = JsonConvert.DeserializeObject<List<Student>>(JsonS);
        }

        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);

            courseCollection.Courses.Should().BeEmpty();
            courseCollection.Students.Should().BeEmpty();
        }

        [Fact]
        public void RepopulateCourseList_Called_ExpectAPropertyChangedEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            courseDataService.Configure().GetCourses().Returns(deserializedCourses);
            courseDataService.Configure().GetStudents().Returns(deserializedStudents);
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
            courseDataService.Configure().GetCourses().Returns(deserializedCourses);
            courseDataService.Configure().GetStudents().Returns(deserializedStudents);
            var courseCollection = new CourseCollection(courseDataService);
            
            courseCollection.RepopulateCourseList();

            courseCollection.Courses.Should().BeEquivalentTo(deserializedCourses);
            courseCollection.Students.Should().BeEquivalentTo(deserializedStudents);
        }

        [Fact]
        public void AddCourse_Called_ExpectANewCourseAddedToList()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = Substitute.ForPartsOf<Course>("id", "title", 1, CourseType.Discussion, new List<int>());
            
            courseCollection.AddCourse(course);
            
            courseCollection.Courses.ForEach(c => ValidateCourse(course, c));
        }
        
        [Fact]
        public void AddCourse_Called_ExpectCoursesToChange()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = Substitute.ForPartsOf<Course>("id", "title", 1, CourseType.Discussion, new List<int>());
            var coursesWasChanged = false;
            courseCollection.PropertyChanged += (_, __) => coursesWasChanged = true;
            
            courseCollection.AddCourse(course);

            coursesWasChanged.Should().BeTrue();
        }
        
        [Fact]
        public void DeleteCourse_Called_ExpectACourseToBeRemoved()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = Substitute.ForPartsOf<Course>("id", "title", 1, CourseType.Discussion, new List<int>());
            courseCollection.Courses.Add(course);
            
            courseCollection.DeleteCourse(course);

            courseCollection.Courses.Should().BeEmpty();
        }
        
        [Fact]
        public void DeleteCourse_Called_ExpectCoursesToChange()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = Substitute.ForPartsOf<Course>("id", "title", 1, CourseType.Discussion, new List<int>());
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
            course.Type.Should().Be(newCourse.Type);
        }
    }
}