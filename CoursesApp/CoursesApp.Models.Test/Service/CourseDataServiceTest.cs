using System;
using System.Collections.Generic;
using CoursesApp.Models.Service;
using FluentAssertions;
using NSubstitute;
using NSubstitute.Extensions;
using Xunit;

namespace CoursesApp.Models.Test.Service
{
    public class CourseDataServiceTest
    {
        [Fact]
        public void GetCourses_Called_ExpectJSONResults()
        {
            var data = Substitute.ForPartsOf<CourseDataService>();
            var fakeCourseList = new List<Course>();
            data.Configure().GetCourses().Returns(fakeCourseList);

            data.GetCourses().Should().Equal(fakeCourseList);
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