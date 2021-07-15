using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CoursesApp.Models.Service;
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

        [Fact]
        public void Courses_RefreshCourseList_ExpectAnEventWithPopulatedData()
        {
            var courseCollection = new CourseCollection();
            var courseData = new CourseDataService();
            var listUpdated = false;
            courseCollection.Courses.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Reset)
                {
                    listUpdated = true;
                }
            };
            
            courseCollection.RepopulateCourseList();

            listUpdated.Should().BeTrue();
            courseCollection.Courses.Should().BeEquivalentTo(courseData.GetCourses());
        }
    }
}