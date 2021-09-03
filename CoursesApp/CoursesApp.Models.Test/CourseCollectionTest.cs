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
            courseCollection.CoursesExist.Should().BeFalse();
        }

        [Fact]
        public void RepopulateCourseList_Called_ExpectAPropertyChangedEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            courseDataService.Configure().GetCourses().Returns(deserializedCourses);
            courseDataService.Configure().GetStudents().Returns(deserializedStudents);
            var courseCollection = new CourseCollection(courseDataService);
            var coursesWasChanged = false;
            var studentsWasChanged = false;
            courseCollection.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CourseCollection.Courses)) coursesWasChanged = true;
                if (args.PropertyName == nameof(CourseCollection.Students)) studentsWasChanged = true;
            };
                
            courseCollection.RepopulateCourseList();

            coursesWasChanged.Should().BeTrue();
            studentsWasChanged.Should().BeTrue();
        }

        [Fact]
        public void RepopulateCourseList_Called_ExpectCoursesAndStudentsUpdated()
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
        public void AddCourse_Called_ExpectCoursesPropertyChange()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = Substitute.ForPartsOf<Course>(1, "title", 1, CourseType.Discussion, new List<int>());
            var coursesWasChanged = false;
            courseCollection.PropertyChanged += (_, __) => coursesWasChanged = true;
            
            courseCollection.AddCourse(course);

            coursesWasChanged.Should().BeTrue();
        }

        [Fact]
        public void AddCourse_Called_ExpectANewCourseAddedToList()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = Substitute.ForPartsOf<Course>(1, "title", 1, CourseType.Discussion, new List<int>());
            var courses = new List<Course> { course };
            
            courseCollection.AddCourse(course);
            
            courseCollection.Courses.Should().BeEquivalentTo(courses);
        }
        
        [Fact]
        public void DeleteCourse_Called_ExpectCoursesPropertyChangeEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = Substitute.ForPartsOf<Course>(1, "title", 1, CourseType.Discussion, new List<int>());
            var coursesWasChanged = false;
            courseCollection.PropertyChanged += (_, __) => coursesWasChanged = true;
            
            courseCollection.DeleteCourse(course);

            coursesWasChanged.Should().BeTrue();
        }
        
        [Fact]
        public void DeleteCourse_Called_ExpectACourseToBeRemoved()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = Substitute.ForPartsOf<Course>(1, "title", 1, CourseType.Discussion, new List<int>());
            courseCollection.Courses.Add(course);
            
            courseCollection.DeleteCourse(course);

            courseCollection.Courses.Should().BeEmpty();
        }

        [Fact]
        public void GetNextCourseId_NoCoursesInList_ReturnsTheNumberOne()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);

            var result = courseCollection.GetNextCourseId();

            result.Should().Be(1);
        }

        [Fact]
        public void GetNextCourseId_Called_IncrementedValueReturned()
        {
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int>());
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);

            courseCollection.AddCourse(course);

            var result = courseCollection.GetNextCourseId();

            result.Should().Be(2);
        }

        [Fact]
        public void UpdateCoursesExist_Called_ExpectCoursesExistPropertyChanged()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var wasCoursesExistUpdated = false;

            courseCollection.PropertyChanged += (sender, args) =>
            {
                wasCoursesExistUpdated = true;
            };

            courseCollection.UpdateCoursesExist();

            wasCoursesExistUpdated.Should().BeTrue();
        }
        
        [Fact]
        public void UpdateCoursesExist_Called_ExpectCoursesExistUpdated()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            courseCollection.Courses.Add(new Course(1));

            courseCollection.UpdateCoursesExist();

            courseCollection.CoursesExist.Should().BeTrue();
        }

        // I tested the property change and the two List additions with one test here:
        [Fact]
        public void AddStudent_Called_ExpectUpdatesStudentsAndCoursesAndEmitsAPropertyChanged()
        {
            var course = Substitute.ForPartsOf<Course>(1, "title", 2, CourseType.Lab, null);
            var student = new Student(1, "Joe", 25, "Liberal Arts");
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var studentsWasUpdated = false;
            var courseCollectionStudents = new List<Student> { student };
            courseCollection.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CourseCollection.Students)) studentsWasUpdated = true;
            };

            courseCollection.AddStudent(course, student);

            courseCollection.Students.Should().BeEquivalentTo(courseCollectionStudents);
            course.Students.Should().BeEquivalentTo(new List<int> { student.Id });
            studentsWasUpdated.Should().BeTrue();
        }
        
        [Fact]
        public void DeleteStudent_StudentInMultipleCourses_ExpectStudentRemovedFromCourseNotRemovedFromCollectionAndPropertyChange()
        {
            var course1 = Substitute.ForPartsOf<Course>(1, "title", 2, CourseType.Lab, new List<int> { 1 });
            var course2 = Substitute.ForPartsOf<Course>(2, "title", 2, CourseType.Lab, new List<int> { 1 });
            var student = new Student(1, "Joe", 25, "Liberal Arts");
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            courseCollection.Courses.Add(course1);
            courseCollection.Courses.Add(course2);
            courseCollection.Students.Add(student);
            var studentWasRemoved = false;
            courseCollection.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CourseCollection.Students)) studentWasRemoved = true;
            };

            courseCollection.DeleteStudent(course1, student);

            course1.Students.Should().BeEmpty();
            courseCollection.Students.Should().BeEquivalentTo(new List<Student>{student});
            studentWasRemoved.Should().BeTrue();
        }

        [Fact]
        public void DeleteStudent_StudentNotInMultipleCourses_ExpectStudentRemovedFromCollectionStudentRemovedFromCourseAndPropertyChange()
        {
            var course = Substitute.ForPartsOf<Course>(1, "title", 2, CourseType.Lab, new List<int> { 1 });
            var student = new Student(1, "Joe", 25, "Liberal Arts");
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            courseCollection.Courses.Add(course);
            courseCollection.Students.Add(student);
            var studentWasRemoved = false;
            courseCollection.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CourseCollection.Students)) studentWasRemoved = true;
            };

            courseCollection.DeleteStudent(course, student);

            course.Students.Should().BeEmpty();
            courseCollection.Students.Should().BeEmpty();
            studentWasRemoved.Should().BeTrue();
        }

        [Fact]
        public void GetStudent_Called_ExpectReturnsAStudentObjectThatMatchesIdParam()
        {
            var course = Substitute.ForPartsOf<Course>(1, "title", 2, CourseType.Lab, new List<int> { 1 });
            var student = new Student(1, "Joe", 25, "Liberal Arts");
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            courseCollection.Courses.Add(course);
            courseCollection.Students.Add(student);

            var results = courseCollection.GetStudent(1);

            results.Should().BeEquivalentTo(student);
        }
    }
}