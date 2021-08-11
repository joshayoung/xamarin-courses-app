using System;
using System.Collections.Generic;
using CoursesApp.Models.Service;
using FluentAssertions;
using Xunit;

namespace CoursesApp.Models.Test.Service
{
    public class CourseDataServiceTest
    {
        [Fact]
        public void GetCourses_Called_ExpectJSONResults()
        {
            var data = new CourseDataService();

            var results = data.GetCourses();

            results.Should().BeOfType(typeof(List<Course>));
        }

        [Fact]
        public void GetCourses_CalledWithBadPath_ExpectErrorMessage()
        {
            var service = new CourseDataService();

            Action testAction = () => service.GetCourses("bad path");

            testAction.Should().Throw<InvalidOperationException>()
                .WithMessage("Error reading JSON file");
        }
    }
}