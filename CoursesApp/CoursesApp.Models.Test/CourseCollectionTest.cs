using System.Collections.ObjectModel;
using CoursesApp.Models.Service;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CoursesApp.Models.Test
{
    public class CourseCollectionTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);

            courseCollection.Courses.Should().BeEquivalentTo(new ObservableCollection<Course>());
        }
    }
}