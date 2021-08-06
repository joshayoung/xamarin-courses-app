using System.Collections.Generic;
using CoursesApp.Models;
using CoursesApp.Models.Service;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CoursesApp.ViewModels.Test
{
    public class StudentViewModelTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            var student = new Student("Joe", 31, "Physics");
            var course = Substitute.ForPartsOf<Course>("id", "title", 1, CourseType.Discussion, new List<Student>(0));
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var courseViewModel = Substitute.ForPartsOf<CourseViewModel>(course, courseCollection);
            var studentViewModel = new StudentViewModel(student, courseViewModel);

            var ages = new List<int>()
            {
                student.Age,
                1,
                2,
                3
            };

            studentViewModel.Ages.Should().BeEquivalentTo(ages);
            studentViewModel.Name.Should().Be(student.Name);
            studentViewModel.Age.Should().Be(student.Age);
            studentViewModel.Major.Should().Be(student.Major);
        }

        [Fact]
        public void ViewModel_PropertyChanged_ExpectPropertyChangedEvents()
        {
            string name = "Joe";
            int age = 31;
            string major = "Physics";
            var student = new Student(name, age, major);
            var course = Substitute.ForPartsOf<Course>("id", "title", 1, CourseType.Discussion, new List<Student>(0));
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var courseViewModel = Substitute.ForPartsOf<CourseViewModel>(course, courseCollection);
            var studentViewModel = new StudentViewModel(student, courseViewModel);
            var nameWasChanged = false;
            var ageWasChanged = false;
            var majorWasChanged = false;

            studentViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(studentViewModel.Name)) nameWasChanged = true;
                if (args.PropertyName == nameof(studentViewModel.Age)) ageWasChanged = true;
                if (args.PropertyName == nameof(studentViewModel.Major)) majorWasChanged = true;
            };

            studentViewModel.Name = "new name";
            studentViewModel.Age = 38;
            studentViewModel.Major = "Science";

            nameWasChanged.Should().BeTrue();
            ageWasChanged.Should().BeTrue();
            majorWasChanged.Should().BeTrue();
        }

        [Fact]
        public void Model_PropertyChanged_ExpectPropertyChangedEvent()
        {
            string name = "Joe";
            int age = 31;
            string major = "Physics";
            var student = new Student(name, age, major);
            var course = Substitute.ForPartsOf<Course>("id", "title", 1, CourseType.Discussion, new List<Student>(0));
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var courseViewModel = Substitute.ForPartsOf<CourseViewModel>(course, courseCollection);
            var studentViewModel = new StudentViewModel(student, courseViewModel);
            var wasChanged = false;

            studentViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(studentViewModel.Name))
                {
                    wasChanged = true;
                }
            };

            student.Name = "new name";
            wasChanged.Should().BeTrue();
        }
    }
}