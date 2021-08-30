using System.Collections.Generic;
using System.Linq;
using CoursesApp.Models;
using CoursesApp.Models.Service;
using FluentAssertions;
using NSubstitute;
using NSubstitute.Extensions;
using Xunit;

namespace CoursesApp.ViewModels.Test
{
    public class StudentViewModelTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            var student = new Student(1, "Joe", 31, "Physics");
            var course = Substitute.ForPartsOf<Course>(1, "title", 1, CourseType.Discussion, new List<int>());
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var studentViewModel = new StudentViewModel(student, course, courseCollection);

            studentViewModel.Name.Should().Be(student.Name);
            studentViewModel.Age.Should().Be(student.Age);
            studentViewModel.Major.Should().Be(student.Major);
        }

        [Fact]
        public void ViewModel_PropertyChanged_ExpectPropertyChangedEvents()
        {
            string name = "Joe";
            int age = 31;
            string major = "Physics";
            var student = new Student(1, name, age, major);
            var course = Substitute.ForPartsOf<Course>(1, "title", 1, CourseType.Discussion, new List<int>());
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var studentViewModel = new StudentViewModel(student, course, courseCollection);
            var nameWasChanged = false;
            var ageWasChanged = false;
            var majorWasChanged = false;

            studentViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(studentViewModel.Name)) nameWasChanged = true;
                if (args.PropertyName == nameof(studentViewModel.Age)) ageWasChanged = true;
                if (args.PropertyName == nameof(studentViewModel.Major)) majorWasChanged = true;
            };

            studentViewModel.Name = "new name";
            studentViewModel.Age = 38;
            studentViewModel.Major = "Science";

            nameWasChanged.Should().BeTrue();
            ageWasChanged.Should().BeTrue();
            majorWasChanged.Should().BeTrue();
        }

        [Fact]
        public void Model_PropertyChanged_ExpectPropertyChangedEvent()
        {
            string name = "Joe";
            int age = 31;
            string major = "Physics";
            var student = new Student(1, name, age, major);
            var course = Substitute.ForPartsOf<Course>(1, "title", 1, CourseType.Discussion, new List<int>());
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var studentViewModel = new StudentViewModel(student, course, courseCollection);
            var nameWasChanged = false;
            var ageWasChanged = false;
            var majorWasChanged = false;

            studentViewModel.PropertyChanged += (sender, args) =>
            {
                // TODO: Are you testing this correctly?
                if (args.PropertyName == nameof(studentViewModel.Name)) nameWasChanged = true;
                if (args.PropertyName == nameof(studentViewModel.Age)) ageWasChanged = true;
                if (args.PropertyName == nameof(studentViewModel.Major)) majorWasChanged = true;
            };

            student.Name = "new name";
            student.Age = 30;
            student.Major = "Math";

            nameWasChanged.Should().BeTrue();
            ageWasChanged.Should().BeTrue();
            majorWasChanged.Should().BeTrue();
        }

        [Fact]
        public void AddStudent_Called_ExpectCallsCorrectMethodCalledWithValue()
        {
            var student1 = new Student(1, "Joe", 31, "Physics");
            var student2 = new Student(2, "Joe", 31, "Physics");
            var course = new Course(1, "title", 1, CourseType.Discussion);
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            courseCollection.Courses.Add(course);
            courseCollection.Students.Add(student1);
            var studentViewModel = new StudentViewModel(student2, course, courseCollection);
            
            studentViewModel.AddStudent();
            
            courseCollection.Received().AddStudent(course, student2);
            // TODO: This does not work?
            // courseCollection.Received().AddStudent(Arg.Is(course), Arg.Is(student2));
        }

        [Fact]
        public void DeleteStudent_Called_ExpectCorrectMethodCalledWithValue()
        {
            string name = "Joe";
            int age = 31;
            string major = "Physics";
            var student = new Student(1, name, age, major);
            var course = Substitute.ForPartsOf<Course>(1, "title", 1, CourseType.Discussion, new List<int>());
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var studentViewModel = new StudentViewModel(student, course, courseCollection);

            studentViewModel.DeleteStudent();

            courseCollection.Received().DeleteStudent(course, student);
            
            // TODO: This is not working:
            // courseCollection.Received().DeleteStudent(Arg.Is(course), Arg.Is(student));
        }
        
        // SaveStudent
        [Fact]
        public void SaveStudent_Called_ExpectStudentUpdate()
        {
            var student = new Student(1, "Joe", 30, "Physics");
            var student2 = new Student(2, "Sally", 20, "Math");
            var course = Substitute.ForPartsOf<Course>(1, "title", 1, CourseType.Discussion, new List<int>());
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            courseCollection.Students.Add(student);
            courseCollection.Students.Add(student2);
            var editedStudent = courseCollection.Students.First(s => s.Id == 2);
            var studentViewModel = new StudentViewModel(student, course, courseCollection);

            studentViewModel.SaveStudent(2, studentViewModel);

            editedStudent.Name.Should().Be(studentViewModel.Name);
            editedStudent.Age.Should().Be(studentViewModel.Age);
            editedStudent.Major.Should().Be(studentViewModel.Major);
        }

        [Fact]
        public void EditStudentCopy_Called_ExpectNewVmWithStudentValues()
        {
            string name = "Joe";
            int age = 31;
            string major = "Physics";
            var student = new Student(1, name, age, major);
            var course = Substitute.ForPartsOf<Course>(1, "title", 1, CourseType.Discussion, new List<int>());
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var studentViewModel = new StudentViewModel(student, course, courseCollection);
            var vm = new StudentViewModel(new Student(student.Id, student.Name, student.Age, student.Major), course,
                courseCollection);

            var result = studentViewModel.EditStudentCopy();

            result.Should().BeEquivalentTo(vm);
        }
    }
}