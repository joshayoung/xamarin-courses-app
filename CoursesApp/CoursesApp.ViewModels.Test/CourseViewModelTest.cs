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
        // Tests the default state of my object:
        [Fact]
        public void Constructor_DefaultParams_ExpectAssignment()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Discussion);

            var courseViewModel = new CourseViewModel(course, courseCollection);

            courseViewModel.StudentsExist.Should().BeFalse();
            courseViewModel.NumberOfStudents.Should().Be(0);
            courseViewModel.AverageStudentAge.Should().Be(0);
            courseViewModel.OldestStudent.Should().Be("");
            courseViewModel.Id.Should().Be(course.Id);
            courseViewModel.Title.Should().Be(course.Title);
            courseViewModel.Length.Should().Be(course.Length);
            courseViewModel.Students.Should().BeEmpty();
            courseViewModel.Type.Should().Be(course.Type);
        }

        // Tests the `RefreshStudents()` call in my constructor:
        [Fact]
        public void Constructor_WithData_ExpectStudentsPopulated()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int> { 1 });
            var student1 = new Student(1, "one", 18, "Liberal Studies");
            courseCollection.Courses.Add(course);
            courseCollection.Students.Add(student1);
            var students = new List<StudentViewModel>
            {
                new StudentViewModel(student1, course, courseCollection)
            };
            
            var courseViewModel = new CourseViewModel(course, courseCollection);

            courseViewModel.Students.Should().BeEquivalentTo(students);
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
        
        //Tests view-model Property Change Events:
        [Fact]
        public void ViewModel_PropertyChanges_ExpectPropertyChangeEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int> { 1 });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            courseCollection.Students.Add(new Student(1, "name", 20, "major"));
            courseCollection.Students.Add(new Student(2, "name", 21, "major"));
            var wasTitleUpdated = false;
            var wasLengthUpdated = false;
            var wasStudentsUpdated = false;
            var wasTypeUpdated = false;

            courseViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CourseViewModel.Title)) wasTitleUpdated = true;
                if (args.PropertyName == nameof(CourseViewModel.Length)) wasLengthUpdated = true;
                if (args.PropertyName == nameof(CourseViewModel.Students)) wasStudentsUpdated = true;
                if (args.PropertyName == nameof(CourseViewModel.Type)) wasTypeUpdated = true;
            };

            courseViewModel.Title = "new title";
            courseViewModel.Length = 1;
            courseViewModel.Students = new List<StudentViewModel>();
            courseViewModel.Type = CourseType.Discussion;

            wasTitleUpdated.Should().BeTrue();
            wasLengthUpdated.Should().BeTrue();
            wasStudentsUpdated.Should().BeTrue();
            wasTypeUpdated.Should().BeTrue();
        }
        
        [Fact]
        public void Model_PropertyChanges_ExpectPropertyChangeEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int> { 1 });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            var wasTitleUpdated = false;
            var wasLengthUpdated = false;
            var wasStudentsUpdated = false;
            var wasTypeUpdated = false;

            courseViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CourseViewModel.Title)) wasTitleUpdated = true;
                if (args.PropertyName == nameof(CourseViewModel.Length)) wasLengthUpdated = true;
                if (args.PropertyName == nameof(CourseViewModel.Students)) wasStudentsUpdated = true;
                if (args.PropertyName == nameof(CourseViewModel.Type)) wasTypeUpdated = true;
            };

            course.Title = "new title";
            course.Length = 1;
            course.Students = new List<int>();
            course.Type = CourseType.Discussion;

            wasTitleUpdated.Should().BeTrue();
            wasLengthUpdated.Should().BeTrue();
            wasStudentsUpdated.Should().BeTrue();
            wasTypeUpdated.Should().BeTrue();
        }

        // Tests Lengths method:
        [Fact]
        public void Lengths_Called_ListOfCourseLengths()
        {
            var lengths = new List<float> { 1, 2, 3, 4 };

            CourseViewModel.Lengths.Should().BeEquivalentTo(lengths);
        }

        // Tests Types method:
        [Fact]
        public void Types_Called_ExpectListOfTypes()
        {
            var types = new List<CourseType>
            {
                CourseType.Seminar,
                CourseType.Lab,
                CourseType.Independent,
                CourseType.Lecture,
                CourseType.Discussion
            };

            CourseViewModel.Types.Should().BeEquivalentTo(types);
        }

        // Test `RefreshStudents()` early return with an empty list of Courses:
        [Fact]
        public void RefreshStudents_CourseStudentsIsNull_ExpectStudentsViewModelEmpty()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab);
            var courseViewModel = new CourseViewModel(course, courseCollection);

            courseViewModel.Students.Should().BeEmpty();
        }

        // Test `RefreshStudents()` when their are courses:
        [Fact]
        public void RefreshStudents_CoursesExist_ExpectStudentsPopulated()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int> { 1, 2 });
            var student1 = new Student(1, "one", 18, "Liberal Studies");
            var student2 = new Student(2, "two", 18, "Liberal Studies");
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
        }

        // Tests `AddCourse` call:
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
        
        // Tests `NewStudent` call:
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
        
        // `DeleteCourse` returns the next ID:
        [Fact]
        public void DeleteCourse_Called_ExpectReturnsCorrectId()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int> { 1, 2 });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            
            courseViewModel.DeleteCourse();
            
            courseCollection.Received().DeleteCourse(course);
        }
        
        // `EditCourseCopy` returns a new vm with a copy of data:
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
        
        // `SaveCourse` modifies course based off ID passed in:
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