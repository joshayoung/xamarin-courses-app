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
    }
}