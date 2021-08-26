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
            courseCollectionViewModel.CoursesExist.Should().BeFalse();
        }

        [Fact]
        public void Constructor_RefreshCourses_ExpectAssignment()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, new List<int>());
            courseCollection.AddCourse(course);
            var courseViewModelList = new List<CourseViewModel>
            {
                new CourseViewModel(course, courseCollection)
            };

            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);

            courseCollectionViewModel.Courses.Should().BeEquivalentTo(courseViewModelList);
        }

        [Fact]
        public void PropertiesChange_Called_ExpectPropertyChangedEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, new List<int>());
            courseCollection.AddCourse(course);
            var wasIsRefreshingChanged = false;
            var wasCoursesChanged = false;
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            courseCollectionViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CourseCollectionViewModel.IsRefreshing)) wasIsRefreshingChanged = true;
                if (args.PropertyName == nameof(CourseCollectionViewModel.Courses)) wasCoursesChanged = true;
            };

            courseCollectionViewModel.Courses =
                new List<CourseViewModel> { new CourseViewModel(course, courseCollection) };
            courseCollectionViewModel.IsRefreshing = true;

            wasIsRefreshingChanged.Should().BeTrue();
            wasCoursesChanged.Should().BeTrue();
        }
        
        [Fact]
        public void CoursesExist_CoursesUpdated_ExpectPropertyChangedEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, new List<int>());
            var wasCoursesExistChanged = false;
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            courseCollectionViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CourseCollectionViewModel.CoursesExist))
                {
                    wasCoursesExistChanged = true;
                }
            };

            courseCollectionViewModel.Courses =
                new List<CourseViewModel> { new CourseViewModel(course, courseCollection) };

            wasCoursesExistChanged.Should().BeTrue();
        }

        [Fact]
        public void CourseCollection_CoursesChange_ExpectRefreshedCourses()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, new List<int>());
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            var courseViewModelList = new List<CourseViewModel>
            {
                new CourseViewModel(course, courseCollection)
            };

            courseCollection.AddCourse(course);

            // TODO: This could probably do a better job of comparing the object:
            courseCollectionViewModel.Courses.Should().BeEquivalentTo(courseViewModelList);
        }
        
        [Fact]
        public void CoursesExist_NoCourses_ReturnsFalse()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);

            courseCollectionViewModel.CoursesExist.Should().BeFalse();
        }

        [Fact]
        public void CoursesExist_OneOrMoreCourses_ReturnsTrue()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, new List<int>());
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            courseCollection.AddCourse(course);

            courseCollectionViewModel.CoursesExist.Should().BeTrue();
        }

        [Fact]
        public void GetNextCourseId_NoCoursesInList_OneReturned()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);

            var result = courseCollectionViewModel.GetNextCourseId();

            result.Should().Be(1);
        }

        [Fact]
        public void GetNextCourseId_Called_IncrementedValueReturned()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, new List<int>());
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            courseCollection.AddCourse(course);

            var result = courseCollectionViewModel.GetNextCourseId();

            result.Should().Be(2);
        }

        [Fact]
        public void Refresh_Called_SetsCorrectValuesAndCallsCorrectMethod()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, new List<int>());
            var courseCollectionViewModel = Substitute.ForPartsOf<CourseCollectionViewModel>(courseCollection);
            courseCollection.AddCourse(course);

            courseCollectionViewModel.Refresh();
            
            courseCollectionViewModel.Received().IsRefreshing = true;
            courseCollection.Received().RepopulateCourseList();
            courseCollectionViewModel.Received().IsRefreshing = false;
        }
        
        [Fact]
        public void NewCourseViewModel_Called_ReturnsNewCourseViewModel()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            var courseViewModel = new CourseViewModel(new Course("1"), courseCollection);

            var results = courseCollectionViewModel.NewCourseViewModel();

            results.Should().BeEquivalentTo(courseViewModel);
        }
    }
}