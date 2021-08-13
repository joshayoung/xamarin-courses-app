using System;
using System.Collections.Generic;
using CoursesApp.Models;
using CoursesApp.Models.Service;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CoursesApp.ViewModels.Test
{
    public class CourseCollectionViewModelTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);

            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);

            courseCollectionViewModel.Courses.Should().BeEmpty();
        }

        [Fact]
        public void Constructor_NullParams_ExpectException()
        {
            Action testAction = () => new CourseCollectionViewModel(null);

            testAction.Should().Throw<ArgumentException>();
        }

        // [Fact]
        // public void Constructor_Called_ExpectViewModelListUpdatedWithModel()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var course = new Course("1", "title", 2, CourseType.Lab, new List<Student>());
        //     courseCollection.AddCourse(course);
        //     var courseViewModelList = new List<CourseViewModel>
        //     {
        //         new CourseViewModel(course, courseCollection)
        //     };
        //     
        //     var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
        //
        //     courseCollectionViewModel.Courses.Should().BeEquivalentTo(courseViewModelList);
        // }

        [Fact]
        public void Courses_Changed_ExpectPropertyChangedEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var wasCourseChanged = false;
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            courseCollectionViewModel.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(courseCollectionViewModel.Courses)) wasCourseChanged = true;
            };

            courseCollectionViewModel.Courses = new List<CourseViewModel>();

            wasCourseChanged.Should().BeTrue();
        }

        // [Fact]
        // public void ModelCourses_Changes_ExpectViewModelCoursesUpdated()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var wasCourseChanged = false;
        //     var course = new Course("1", "title", 2, CourseType.Lab, new List<Student>());
        //     var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
        //     courseCollectionViewModel.PropertyChanged += (_, args) =>
        //     {
        //         if (args.PropertyName == nameof(CourseCollection.Courses)) wasCourseChanged = true;
        //     };
        //
        //     courseCollection.AddCourse(course);
        //
        //     wasCourseChanged.Should().BeTrue();
        // }

        // [Fact]
        // public void ModelCourses_Changed_ExpectUpdatedViewModelCourseList()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var course = new Course("1", "title", 2, CourseType.Lab, new List<Student>());
        //     var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
        //     var courseViewModelList = new List<CourseViewModel>
        //     {
        //         new CourseViewModel(course, courseCollection)
        //     };
        //
        //     courseCollection.AddCourse(course);
        //
        //     courseCollectionViewModel.Courses.Should().BeEquivalentTo(courseViewModelList);
        // }

        [Fact]
        public void NewCourseViewModel_CalledWithEmptyCourseList_ExpectReturnNewCourseViewModel()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            var courseViewModel = new CourseViewModel(new Course("1"), courseCollection);

            var results = courseCollectionViewModel.NewCourseViewModel();

            results.Should().BeEquivalentTo(courseViewModel);

        }

        // [Fact]
        // public void NewCourseViewModel_CalledWithANonEmptyCourseList_ExpectReturnNewCourseViewModel()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var course = new Course("1", "title", 2, CourseType.Lab, new List<Student>());
        //     var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
        //     var courseViewModel = new CourseViewModel(new Course("2"), courseCollection);
        //
        //     courseCollection.AddCourse(course);
        //
        //     var results = courseCollectionViewModel.NewCourseViewModel();
        //
        //     results.Should().BeEquivalentTo(courseViewModel);
        // }
    }
}