using System.Collections.ObjectModel;
using FluentAssertions;
using Xunit;

namespace CoursesApp.Models.Test
{
    public class CourseCollectionTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            var courseCollection = new CourseCollection();

            courseCollection.Courses.Should().BeEquivalentTo(new ObservableCollection<Course>());
        }
    }
}