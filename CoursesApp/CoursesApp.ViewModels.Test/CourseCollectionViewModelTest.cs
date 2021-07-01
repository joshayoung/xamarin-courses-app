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

            courseCollectionViewModel.LastCourse.Should().BeNull();
            courseCollectionViewModel.CoursesWithMultipleStudents.Should().BeEmpty();
        }
        
        [Fact]
        public void LastCourse_ReloadTheClasses_ExpectPropertyChangeEvent()
        {
            var courseCollection = new CourseCollection();
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            var lastCourse = new Course("Last Course", 4, new List<Student>(), new List<Teacher>(), CourseType.Discussion);
            var courses = new List<Course> { 
                new Course("First Course", 2, new List<Student>(), new List<Teacher>(), CourseType.Lab),
                lastCourse
            };
            var wasChanged = false;
            courseCollectionViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(courseCollectionViewModel.LastCourse))
                {
                    wasChanged = true;
                }
            };
            
            courseCollection.Courses.Clear();
            courses.ForEach(course => courseCollection.Courses.Add(course));
                
            // courseCollectionViewModel.LastCourse.Should().BeEquivalentTo(new CourseViewModel(lastCourse));
            wasChanged.Should().BeTrue();
        }
        
        [Fact]
        public void CoursesWithMultipleStudents_ReloadTheClasses_ExpectCollectionChangedEvent()
        {
            var courseCollection = new CourseCollection();
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            var multipleStudents = new Course(
                "title 1",
                3,
                new List<Student> {new Student("name", 20, "Biology"), new Student("name", 30, "Math")},
                new List<Teacher>(),
                CourseType.Discussion);
            var courses = new List<Course> { 
                new Course("title 2", 2, new List<Student>(), new List<Teacher>(), CourseType.Discussion),
                multipleStudents
            };
            var coursesWithMultipleStudentsViewModels = new List<CourseViewModel>();
            coursesWithMultipleStudentsViewModels.Add(new CourseViewModel(multipleStudents));

            courseCollection.Courses.Clear();
            courses.ForEach(course => courseCollection.Courses.Add(course));
            
            courseCollectionViewModel.CoursesWithMultipleStudents.Should()
                .BeEquivalentTo(coursesWithMultipleStudentsViewModels);
        }
    }
}