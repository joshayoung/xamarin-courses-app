using System.Collections.Generic;
using System.Linq;
using CoursesApp.Models;
using CoursesApp.Models.Service;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CoursesApp.ViewModels.Test
{
    public class CourseViewModelTest
    {
        // Tests the default state of my object:
        [Fact]
        public void Constructor_DefaultParams_ExpectAssignment()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Discussion);

            var courseViewModel = new CourseViewModel(course, courseCollection);

            courseViewModel.StudentsExist.Should().BeFalse();
            courseViewModel.NumberOfStudents.Should().Be(0);
            courseViewModel.AverageStudentAge.Should().Be(0);
            courseViewModel.OldestStudent.Should().Be("");
            courseViewModel.Id.Should().Be(course.Id);
            courseViewModel.Title.Should().Be(course.Title);
            courseViewModel.Length.Should().Be(course.Length);
            courseViewModel.Students.Should().BeEmpty();
            courseViewModel.Type.Should().Be(course.Type);
        }
        
        // Tests the `RefreshStudents()` call in my constructor:
        [Fact]
        public void RefreshStudents_Called_PopulatesStudents()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Discussion, null);

            var student = new Student(1);
            var students = new List<StudentViewModel>()
            {
                new StudentViewModel(student, course, courseCollection)
            };
            courseCollection.AddCourse(course);
            courseCollection.AddStudent(course, student);

            var courseViewModel = new CourseViewModel(course, courseCollection);

            courseViewModel.Students.Should().BeEquivalentTo(students);
        }

        // Tests `StudentsCollectionOnPropertyChanged` in my constructor:
        [Fact]
        public void StudentsCollectionOnPropertyChanged_Called_PopulatesStudents()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Discussion)
            {
                Students = new List<int>(1)
            };
            var student = new Student(1);
            var students = new List<StudentViewModel>()
            {
                new StudentViewModel(student, course, courseCollection)
            };
            courseCollection.AddCourse(course);
            courseCollection.AddStudent(course, student);

            var courseViewModel = new CourseViewModel(course, courseCollection);

            courseViewModel.Students.Should().BeEquivalentTo(students);
        }
        
        // Tests Lengths method:
        [Fact]
        public void Lengths_Called_ListOfCourseLengths()
        {
            var lengths = new List<float> { 1, 2, 3, 4 };

            CourseViewModel.Lengths.Should().BeEquivalentTo(lengths);
        }

        // Tests Types method:
        [Fact]
        public void Types_Called_ExpectListOfTypes()
        {
            var types = new List<CourseType>
            {
                CourseType.Seminar,
                CourseType.Lab,
                CourseType.Independent,
                CourseType.Lecture,
                CourseType.Discussion
            };

            CourseViewModel.Types.Should().BeEquivalentTo(types);
        }
        
        // Test `RefreshStudents()` early return with an empty list of Courses:
        [Fact]
        public void RefreshStudents_CourseStudentsIsNull_ExpectStudentsViewModelEmpty()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab);
            var courseViewModel = new CourseViewModel(course, courseCollection);

            courseViewModel.Students.Should().BeEmpty();
        }
    }
}