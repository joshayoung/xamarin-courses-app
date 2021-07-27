using System.Collections.Generic;
using CoursesApp.Models;
using FluentAssertions;
using Xunit;

namespace CoursesApp.ViewModels.Test
{
    public class CourseViewModelTest
    {
        
        [Fact]
        public void ViewModel_PropertyChanged_ExpectPropertyChangedEvents()
        {
            var title = "title";
            float length = 2;
            List<Student> students = new List<Student>();
            var courseCollection = new CourseCollection();
            CourseType type = CourseType.Lab;
            var titleWasChanged = false;
            var lengthWasChanged = false;
            var studentsWasChanged = false;
            var teachersWasChanged = false;
            var typeWasChanged = false;
            var course = new Course(title, length, students, type);
            var courseViewModel = new CourseViewModel(course, courseCollection);
            courseViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(courseViewModel.Title)) titleWasChanged = true;
                if (args.PropertyName == nameof(courseViewModel.Length)) lengthWasChanged = true;
                if (args.PropertyName == nameof(courseViewModel.Students)) studentsWasChanged = true;
                if (args.PropertyName == nameof(courseViewModel.Type)) typeWasChanged = true;
            };

            courseViewModel.Title = "new title";
            courseViewModel.Length = 3;
            courseViewModel.Students = new List<StudentViewModel>();
            courseViewModel.Type = CourseType.Seminar;

            titleWasChanged.Should().BeTrue();
            lengthWasChanged.Should().BeTrue();
            studentsWasChanged.Should().BeTrue();
            typeWasChanged.Should().BeTrue();
        }
        
        [Fact]
        public void Model_PropertyChanged_ExpectPropertyChangedEvent()
        {
            string title = "title";
            float length = 2;
            List<Student> students = new List<Student>();
            var courseCollection = new CourseCollection();
            CourseType type = CourseType.Lab;
            var course = new Course(title, length, students, type);
            var wasChanged = false;
            var courseViewModel = new CourseViewModel(course, courseCollection);
            courseViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(courseViewModel.Title))
                {
                    wasChanged = true;
                }
            };

            course.Title = "a new title";
        
            wasChanged.Should().BeTrue();
        }
    }
}