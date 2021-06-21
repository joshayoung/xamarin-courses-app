using System;
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
            var courses = new List<Course>();
            var courseCollectionViewModel = new CourseCollectionViewModel(courses);

            courseCollectionViewModel.AllCourses.Should().BeEquivalentTo(courses);
        }
    }
}