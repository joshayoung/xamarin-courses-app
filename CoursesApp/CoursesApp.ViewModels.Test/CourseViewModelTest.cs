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
        [Fact]
        public void ViewModel_PropertyChanged_ExpectPropertyChangedEvents()
        {
            var id = "1";
            var title = "title";
            float length = 2;
            List<Student> students = new List<Student>();
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            CourseType type = CourseType.Lab;
            var idWasChanged = false;
            var titleWasChanged = false;
            var lengthWasChanged = false;
            var studentsWasChanged = false;
            var typeWasChanged = false;
            var course = new Course(id, title, length, type, students);
            var courseViewModel = new CourseViewModel(course, courseCollection);
            courseViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(courseViewModel.Id)) idWasChanged = true;
                if (args.PropertyName == nameof(courseViewModel.Title)) titleWasChanged = true;
                if (args.PropertyName == nameof(courseViewModel.Length)) lengthWasChanged = true;
                if (args.PropertyName == nameof(courseViewModel.Students)) studentsWasChanged = true;
                if (args.PropertyName == nameof(courseViewModel.Type)) typeWasChanged = true;
            };

            courseViewModel.Id = "2";
            courseViewModel.Title = "new title";
            courseViewModel.Length = 3;
            courseViewModel.Students = new List<StudentViewModel>();
            courseViewModel.Type = CourseType.Seminar;

            idWasChanged.Should().BeTrue();
            titleWasChanged.Should().BeTrue();
            lengthWasChanged.Should().BeTrue();
            studentsWasChanged.Should().BeTrue();
            typeWasChanged.Should().BeTrue();
        }

        [Fact]
        public void Model_PropertyChanged_ExpectPropertyChangedEvent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, new List<Student>());
            var wasIdChanged = false;
            var wasTitleChanged = false;
            var wasLengthChanged = false;
            var wasStudentsChanged = false;
            var wasTypeChanged = false;
            var courseViewModel = new CourseViewModel(course, courseCollection);
            courseViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(courseViewModel.Id)) wasIdChanged = true;
                if (args.PropertyName == nameof(courseViewModel.Title)) wasTitleChanged = true;
                if (args.PropertyName == nameof(courseViewModel.Length)) wasLengthChanged = true;
                if (args.PropertyName == nameof(courseViewModel.Students)) wasStudentsChanged = true;
                if (args.PropertyName == nameof(courseViewModel.Type)) wasTypeChanged = true;
            };

            course.Id = "2";
            course.Title = "a new title";
            course.Length = 3;
            course.Students = new List<Student>();
            course.Type = CourseType.Discussion;

            wasIdChanged.Should().BeTrue();
            wasTitleChanged.Should().BeTrue();
            wasLengthChanged.Should().BeTrue();
            wasStudentsChanged.Should().BeTrue();
            wasTypeChanged.Should().BeTrue();
        }

        [Fact]
        public void StudentOnPropertyChanged_Called_ExpectAverageAgeUpdate()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
            var wasAgeChanged = false;
            var courseViewModel = new CourseViewModel(course, courseCollection);
            course.Students.ForEach(student => student.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Student.Age)) wasAgeChanged = true;
            });

            courseViewModel.Students.First().Age = 40;

            wasAgeChanged.Should().BeTrue();
        }

        [Fact]
        public void AddCourse_Called_ExpectCallsCorrectMethodWithCorrectParam()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            
            courseViewModel.AddCourse();

            courseCollection.Received().AddCourse(course);
        }
        
        [Fact]
        public void EditCourse_Called_ExpectCallsCorrectMethodWithCorrectParam()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            
            courseViewModel.EditCourse();

            courseCollection.Received().EditCourse(course);
        }
        
        [Fact]
        public void AddStudent_Called_ExpectCallsCorrectMethodWithCorrectParam()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course =
                Substitute.ForPartsOf<Course>("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
            var student = new Student();
            var courseViewModel = new CourseViewModel(course, courseCollection);
            
            courseViewModel.AddStudent(student);

            course.Received().AddStudent(student);
        }
        
        [Fact]
        public void DeleteStudent_Called_ExpectCallsCorrectMethodWithCorrectParam()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course =
                Substitute.ForPartsOf<Course>("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
            var student = new Student();
            var courseViewModel = new CourseViewModel(course, courseCollection);
            
            courseViewModel.DeleteStudent(student);

            course.Received().DeleteStudent(student);
        }

        [Fact]
        public void NewStudent_Called_ExpectAStudentViewModel()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            var studentViewModel = new StudentViewModel(new Student(), courseViewModel);
            
            var result = courseViewModel.NewStudent();
            
            result.Should().BeEquivalentTo(studentViewModel);
        }
        
        [Fact]
        public void DeleteCourse_Called_ExpectCallsCorrectMethodWithCorrectParam()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
            var courseViewModel = new CourseViewModel(course, courseCollection);
            
            courseViewModel.DeleteCourse();

            courseCollection.Received().DeleteCourse(course);
        }
    }
}