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
        public void Constructor_DefaultParams_ExpectAssignment()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);

            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);

            courseCollectionViewModel.IsRefreshing.Should().BeFalse();
            courseCollectionViewModel.Courses.Should().BeEmpty();
        }
        
        // Tests: 'courseCollection.PropertyChanged += CoursesCollectionOnPropertyChanged;' in constructor
        [Fact]
        public void CourseCollection_CoursesChange_ExpectRefreshedCourses()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int>());
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            var courseViewModelList = new List<CourseViewModel> { new CourseViewModel(course, courseCollection) };

            courseCollection.AddCourse(course);

            courseCollectionViewModel.Courses.Should().BeEquivalentTo(courseViewModelList);
            courseCollectionViewModel.CoursesExist.Should().BeTrue();
        }

        [Fact]
        public void Constructor_RefreshCourses_ExpectAssignment()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int>());
            courseCollection.AddCourse(course);
            var courseViewModelList = new List<CourseViewModel> { new CourseViewModel(course, courseCollection) };

            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);

            courseCollectionViewModel.Courses.Should().BeEquivalentTo(courseViewModelList);
            courseCollectionViewModel.CoursesExist.Should().BeTrue();
        }
        
        [Fact]
        public void IsRefreshingViewModelPropertyChanges_Called_ExpectPropertyChangedEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int>());
            courseCollection.AddCourse(course);
            var wasIsRefreshingChanged = false;
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            courseCollectionViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CourseCollectionViewModel.IsRefreshing)) wasIsRefreshingChanged = true;
            };

            courseCollectionViewModel.IsRefreshing = true;

            wasIsRefreshingChanged.Should().BeTrue();
        }
        
        [Fact]
        public void ModelPropertyChanges_Called_ExpectPropertyChangedEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int>());
            var wasCoursesChanged = false;
            var wasCoursesExistChanged = false;
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            courseCollectionViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CourseCollection.Courses)) wasCoursesChanged = true;
                if (args.PropertyName == nameof(CourseCollection.CoursesExist)) wasCoursesExistChanged = true;
            };

            courseCollection.AddCourse(course);

            wasCoursesChanged.Should().BeTrue();
            wasCoursesExistChanged.Should().BeTrue();
        }

        [Fact]
        public void GetNextId_Called_ExpectCorrectValueReturned()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int>());
            courseCollection.AddCourse(course);
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection)
            {
                Courses = new List<CourseViewModel> { new CourseViewModel(course, courseCollection) }
            };

            var result = courseCollectionViewModel.GetNextId;

            result.Should().Be(2);
        }
        
        [Fact]
        public void CoursesExist_Called_ReturnsCorrectResults()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int>());
            courseCollection.Courses.Add(course);
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);

            courseCollectionViewModel.CoursesExist.Should().BeTrue();
        }

        [Fact]
        public void Refresh_Called_SetsCorrectValuesAndCallsCorrectMethod()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int>());
            var courseCollectionViewModel = Substitute.ForPartsOf<CourseCollectionViewModel>(courseCollection);
            courseCollection.AddCourse(course);

            courseCollectionViewModel.Refresh();
            
            courseCollectionViewModel.Received().IsRefreshing = true;
            courseCollection.Received().RepopulateCourseList();
            courseCollectionViewModel.Received().IsRefreshing = false;
        }
        
        [Fact]
        public void NewCourseViewModel_Called_ReturnsCorrectObject()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            var courseViewModel = new CourseViewModel(new Course(1), courseCollection);

            var results = courseCollectionViewModel.NewCourseViewModel();

            results.Should().BeEquivalentTo(courseViewModel);
        }
    }
}