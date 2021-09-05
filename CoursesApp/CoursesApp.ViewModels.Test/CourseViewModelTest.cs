using System.Collections.Generic;
using System.Linq;
using CoursesApp.Models;
using CoursesApp.Models.Service;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CoursesApp.ViewModels.Test
{
    public class CourseViewModelTest
    {
        // Test 'if (!courseCollection.Courses.Contains(course)) return;' in RefreshStudents():
        [Fact]
        public void Constructor_DefaultParams_ExpectAssignment()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Discussion);

            var courseViewModel = new CourseViewModel(course, courseCollection);

            courseViewModel.Id.Should().Be(course.Id);
            courseViewModel.Title.Should().Be(course.Title);
            courseViewModel.Length.Should().Be(course.Length);
            courseViewModel.Type.Should().Be(course.Type);
            courseViewModel.Students.Should().BeEmpty();
            courseViewModel.AverageStudentAge.Should().Be(0);
            courseViewModel.OldestStudent.Should().Be("");
            courseViewModel.NumberOfStudents.Should().Be(0);
            courseViewModel.StudentsExist.Should().BeFalse();
        }
        
        // Tests the `RefreshStudents()` call in my constructor:
        [Fact]
        public void Constructor_WithData_ExpectStudentsPopulated()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int> { 1, 2 });
            var student1 = new Student(1, "one", 18, "Liberal Studies");
            var student2 = new Student(2, "two", 32, "Liberal Studies");
            courseCollection.Courses.Add(course);
            courseCollection.Students.Add(student1);
            courseCollection.Students.Add(student2);
            var students = new List<StudentViewModel>
            {
                new StudentViewModel(student1, course, courseCollection),
                new StudentViewModel(student2, course, courseCollection),
            };
            
            var courseViewModel = new CourseViewModel(course, courseCollection);

            courseViewModel.Students.Should().BeEquivalentTo(students);
            courseViewModel.AverageStudentAge.Should().Be(25);
            courseViewModel.OldestStudent.Should().Be("two");
            courseViewModel.NumberOfStudents.Should().Be(2);
            courseViewModel.StudentsExist.Should().BeTrue();
        }
        
        // Test `RefreshStudents()` early return with an empty list of Courses:
        // (Technically this is tested in my constructor tests implicitly)
        [Fact]
        public void RefreshStudents_CourseStudentsIsNull_ExpectStudentsViewModelEmpty()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab);
            var courseViewModel = new CourseViewModel(course, courseCollection);

            courseViewModel.Students.Should().BeEmpty();
        }
         
        // Test the VM Property Change.
        // I only need to test this one, because this is the only property using: 'NotifyPropertyChanged()'.
        [Fact]
        public void Student_PropertyChanges_ExpectPropertyChangeEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int> { 1 });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            courseCollection.Students.Add(new Student(1, "name", 20, "major"));
            var wasStudentsUpdated = false;
            courseViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CourseViewModel.Students)) wasStudentsUpdated = true;
            };

            courseViewModel.Students = new List<StudentViewModel>();

            wasStudentsUpdated.Should().BeTrue();
        }

        [Fact]
        public void Students_Set_ExpectCallsToMethods()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = Substitute.ForPartsOf<Course>(1, "title", 2, CourseType.Lab, new List<int> { 1 });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            courseCollection.Students.Add(new Student(1, "name", 20, "major"));

            courseViewModel.Students = new List<StudentViewModel>();
            
            course.Received().UpdateAverageAge(courseCollection);
            course.Received().UpdateOldestStudent(courseCollection);
            course.Received().UpdateStudentCount();
            course.Received().UpdateStudentsExist();
        }
        
        [Fact]
        public void Model_PropertyChanges_ExpectPropertyChangeEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            courseCollection.Students.Add(new Student(1));
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int> { 1 });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            var wasTitleUpdated = false;
            var wasLengthUpdated = false;
            var wasTypeUpdated = false;
            var wasAverageStudentAgeUpdate = false;
            var wasOldestStudentUpdated = false;
            var wasNumberOfStudentsUpdated = false;
            var wasStudentsExistUpdated = false;
            courseViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Course.Title)) wasTitleUpdated = true;
                if (args.PropertyName == nameof(Course.Length)) wasLengthUpdated = true;
                if (args.PropertyName == nameof(Course.Type)) wasTypeUpdated = true;
                if (args.PropertyName == nameof(Course.AverageStudentAge)) wasAverageStudentAgeUpdate = true;
                if (args.PropertyName == nameof(Course.OldestStudent)) wasOldestStudentUpdated = true;
                if (args.PropertyName == nameof(Course.NumberOfStudents)) wasNumberOfStudentsUpdated = true;
                if (args.PropertyName == nameof(Course.StudentsExist)) wasStudentsExistUpdated = true;
            };

            course.Title = "new title";
            course.Length = 1;
            course.Type = CourseType.Discussion;
            course.UpdateAverageAge(courseCollection);
            course.UpdateOldestStudent(courseCollection);
            course.UpdateStudentCount();
            course.UpdateStudentsExist();
            
            wasTitleUpdated.Should().BeTrue();
            wasLengthUpdated.Should().BeTrue();
            wasTypeUpdated.Should().BeTrue();
            wasAverageStudentAgeUpdate.Should().BeTrue();
            wasOldestStudentUpdated.Should().BeTrue();
            wasNumberOfStudentsUpdated.Should().BeTrue();
            wasStudentsExistUpdated.Should().BeTrue();
        }
        
        // Tests `StudentsCollectionOnPropertyChanged` call in constructor:
        [Fact]
        public void StudentsCollectionOnPropertyChanged_StudentsAdded_ExpectRefreshStudentsCalled()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int> { 1 });
            var student1 = new Student(1, "one", 18, "Liberal Studies");
            var student2 = new Student(2, "two", 18, "Liberal Studies");
            courseCollection.Courses.Add(course);
            courseCollection.Students.Add(student1);
            var students = new List<StudentViewModel>
            {
                new StudentViewModel(student1, course, courseCollection),
                new StudentViewModel(student2, course, courseCollection)
            };
            
            var courseViewModel = new CourseViewModel(course, courseCollection);
            courseCollection.AddStudent(course, student2);

            courseViewModel.Students.Should().BeEquivalentTo(students);
        }
        
        [Fact]
        public void Lengths_Called_ReturnsCorrectResults()
        {
            var results = new List<float> { 1, 2, 3, 4 };

            CourseViewModel.Lengths.Should().BeEquivalentTo(results);
        }

        [Fact]
        public void Types_Called_ReturnsCorrectResults()
        {
            var results = new List<CourseType>
            {
                CourseType.Seminar,
                CourseType.Lab,
                CourseType.Independent,
                CourseType.Lecture,
                CourseType.Discussion
            };

            CourseViewModel.Types.Should().BeEquivalentTo(results);
        }
        
        [Fact]
        public void AddCourse_Called_ExpectCallsCollectionWithCourse()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int> { 1, 2 });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            
            courseViewModel.AddCourse();
            
            courseCollection.Received().AddCourse(course);
        }
        
        [Fact]
        public void NewStudent_Called_ExpectReturnsCorrectResults()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int> { 1, 2 });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            var student = new Student(1);
            courseCollection.Students.Add(student);
            var newStudent = new StudentViewModel(new Student(2), course, courseCollection);
                        
            var results = courseViewModel.NewStudent();
                        
            results.Should().BeEquivalentTo(newStudent);
        }
        
        [Fact]
        public void DeleteCourse_Called_ExpectCallsCorrectMethod()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int> { 1, 2 });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            
            courseViewModel.DeleteCourse();
            
            courseCollection.Received().DeleteCourse(course);
        }
        
        [Fact]
        public void EditCourseCopy_Called_ExpectReturnsNewVM()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int> { 1, 2 });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            var newVm = new CourseViewModel(new Course(1, course.Title, course.Length, course.Type), courseCollection);
            
            var result = courseViewModel.EditCourseCopy(1);

            result.Should().BeEquivalentTo(newVm);
        }
        
        [Fact]
        public void SaveCourse_Called_ExpectModifiesCourse()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab);
            var course2 = new Course(2, "title2", 3, CourseType.Discussion);
            courseCollection.Courses.Add(course);
            courseCollection.Courses.Add(course2);
            var courseViewModel = new CourseViewModel(course, courseCollection);
            const int id = 2;
            var cs = courseCollection.Courses.First(c => c.Id == id);
            
            courseViewModel.SaveCourse(id);

            cs.Title.Should().Be(course.Title);
            cs.Length.Should().Be(course.Length);
            cs.Type.Should().Be(course.Type);
        }
    }
}