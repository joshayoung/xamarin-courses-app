using System.Collections.Generic;
using CoursesApp.Models;
using FluentAssertions;
using Xunit;

namespace CoursesApp.ViewModels.Test
{
    public class CourseCollectionViewModelTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            var courseCollection = new CourseCollection();
            
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);

            courseCollectionViewModel.Courses.Should().BeEmpty();
        }
    }
}