using System.Collections.Generic;
using CoursesApp.Models.Helpers;
using FluentAssertions;
using Xunit;

namespace CoursesApp.Models.Test.Helpers
{
    public class ModelHelperTest
    {
        [Fact]
        public void StudentAges_Called_ExpectCorrectResults()
        {
            var ages = new List<int>
                {
                    17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30
                };

            ModelHelper.StudentAges().Should().BeEquivalentTo(ages);
        }

        [Fact]
        public void CourseTypes_Called_ExpectCorrectResults()
        {
            var courseTypes = new List<CourseType>
                {
                    CourseType.Seminar,
                    CourseType.Lab,
                    CourseType.Independent,
                    CourseType.Lecture,
                    CourseType.Discussion
                };

            ModelHelper.CourseTypes.Should().BeEquivalentTo(courseTypes);
        }

        [Fact]
        public void CourseLengths_Called_ExpectCorrectResults()
        {
            var courseLengths = new List<float> { 1, 2, 3, 4 };

            ModelHelper.CourseLengths.Should().BeEquivalentTo(courseLengths);
        }
    }
}