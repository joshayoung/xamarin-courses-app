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
        // [Fact]
        // public void ViewModel_PropertyChanged_ExpectPropertyChangedEvents()
        // {
        //     var id = "1";
        //     var title = "title";
        //     float length = 2;
        //     List<Student> students = new List<Student>();
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     CourseType type = CourseType.Lab;
        //     var idWasChanged = false;
        //     var titleWasChanged = false;
        //     var lengthWasChanged = false;
        //     var studentsWasChanged = false;
        //     var typeWasChanged = false;
        //     var course = new Course(id, title, length, type, students);
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //     courseViewModel.PropertyChanged += (sender, args) =>
        //     {
        //         if (args.PropertyName == nameof(courseViewModel.Id)) idWasChanged = true;
        //         if (args.PropertyName == nameof(courseViewModel.Title)) titleWasChanged = true;
        //         if (args.PropertyName == nameof(courseViewModel.Length)) lengthWasChanged = true;
        //         if (args.PropertyName == nameof(courseViewModel.Students)) studentsWasChanged = true;
        //         if (args.PropertyName == nameof(courseViewModel.Type)) typeWasChanged = true;
        //     };
        //
        //     courseViewModel.Id = "2";
        //     courseViewModel.Title = "new title";
        //     courseViewModel.Length = 3;
        //     courseViewModel.Students = new List<StudentViewModel>();
        //     courseViewModel.Type = CourseType.Seminar;
        //
        //     idWasChanged.Should().BeTrue();
        //     titleWasChanged.Should().BeTrue();
        //     lengthWasChanged.Should().BeTrue();
        //     studentsWasChanged.Should().BeTrue();
        //     typeWasChanged.Should().BeTrue();
        // }

        // [Fact]
        // public void Model_PropertyChanged_ExpectPropertyChangedEvent()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var course = new Course("1", "title", 2, CourseType.Lab, new List<Student>()
        //     );
        //     var wasIdChanged = false;
        //     var wasTitleChanged = false;
        //     var wasLengthChanged = false;
        //     var wasStudentsChanged = false;
        //     var wasTypeChanged = false;
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //     courseViewModel.PropertyChanged += (sender, args) =>
        //     {
        //         if (args.PropertyName == nameof(courseViewModel.Id)) wasIdChanged = true;
        //         if (args.PropertyName == nameof(courseViewModel.Title)) wasTitleChanged = true;
        //         if (args.PropertyName == nameof(courseViewModel.Length)) wasLengthChanged = true;
        //         if (args.PropertyName == nameof(courseViewModel.Students)) wasStudentsChanged = true;
        //         if (args.PropertyName == nameof(courseViewModel.Type)) wasTypeChanged = true;
        //     };
        //
        //     course.Id = "2";
        //     course.Title = "a new title";
        //     course.Length = 3;
        //     course.Students = new List<Student> { new Student("name", 30, "major") };
        //     course.Type = CourseType.Discussion;
        //
        //     wasIdChanged.Should().BeTrue();
        //     wasTitleChanged.Should().BeTrue();
        //     wasLengthChanged.Should().BeTrue();
        //     wasStudentsChanged.Should().BeTrue();
        //     wasTypeChanged.Should().BeTrue();
        // }

        // [Fact]
        // public void AverageStudentAge_PropertyChanged_ExpectPropertyChangedEvent()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var course = new Course("1", "title", 2, CourseType.Lab,
        //         new List<Student> { new Student("name", 30, "major") }
        //     );
        //     var wasAverageStudentAgeChanged = false;
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //     courseViewModel.PropertyChanged += (sender, args) =>
        //     {
        //         if (args.PropertyName == nameof(courseViewModel.AverageStudentAge)) wasAverageStudentAgeChanged = true;
        //     };
        //
        //     course.Students.First().Age = 20;
        //
        //     wasAverageStudentAgeChanged.Should().BeTrue();
        // }

        // [Fact]
        // public void AverageStudentAge_Called_ExpectAverageAgeReturned()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var students = new List<Student>
        //     {
        //         new Student("name", 20, "major"),
        //         new Student("name", 40, "major"),
        //     };
        //     var course = new Course("1", "title", 2, CourseType.Lab, students);
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //
        //     var results = courseViewModel.AverageStudentAge;
        //
        //     results.Should().Be(30);
        // }

        // [Fact]
        // public void SelectedType_Called_ExpectReturnsCorrectValueAndPropertyChange()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var students = new List<Student>
        //     {
        //         new Student("name", 20, "major"),
        //         new Student("name", 40, "major"),
        //     };
        //     var course = new Course("1", "title", 2, CourseType.Lab, students);
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //     var wasSelectedTypeChanged = false;
        //
        //     courseViewModel.PropertyChanged += (sender, args) =>
        //     {
        //         if (args.PropertyName == nameof(CourseViewModel.SelectedType)) wasSelectedTypeChanged = true;
        //     };
        //
        //     courseViewModel.SelectedType.Should().Be(course.Type);
        //     courseViewModel.SelectedType = CourseType.Discussion;
        //     wasSelectedTypeChanged.Should().BeTrue();
        // }
        
        // [Fact]
        // public void SelectedLength_Called_ExpectReturnsCorrectValueAndPropertyChange()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var students = new List<Student>
        //     {
        //         new Student("name", 20, "major"),
        //         new Student("name", 40, "major"),
        //     };
        //     var course = new Course("1", "title", 2, CourseType.Lab, students);
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //     var wasSelectedLengthChanged = false;
        //
        //     courseViewModel.PropertyChanged += (sender, args) =>
        //     {
        //         if (args.PropertyName == nameof(CourseViewModel.SelectedLength)) wasSelectedLengthChanged = true;
        //     };
        //
        //     courseViewModel.SelectedLength.Should().Be(course.Length);
        //     courseViewModel.SelectedLength = 3;
        //     wasSelectedLengthChanged.Should().BeTrue();
        // }

        // [Fact]
        // public void CourseLengthList_Called_ExpectCorrectReturnValue()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var students = new List<Student>
        //     {
        //         new Student("name", 20, "major"),
        //         new Student("name", 40, "major"),
        //     };
        //     var course = new Course("1", "title", 2, CourseType.Lab, students);
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //     var wasSelectedLengthChanged = false;
        //     var courseLengthList = new List<float> { 1, 2, 3, 4 };
        //
        //     courseViewModel.CourseLengthList.Should().BeEquivalentTo(courseLengthList);
        // }

        // [Fact]
        // public void CourseTypesList_Called_ExpectReturnsTheCorrectValue()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var students = new List<Student>
        //     {
        //         new Student("name", 20, "major"),
        //         new Student("name", 40, "major"),
        //     };
        //     var course = new Course("1", "title", 2, CourseType.Lab, students);
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //     var courseTypesList = new List<CourseType>
        //         {
        //             CourseType.Seminar,
        //             CourseType.Lab,
        //             CourseType.Independent,
        //             CourseType.Lecture,
        //             CourseType.Discussion,
        //         };
        //
        //     courseViewModel.CourseTypesList.Should().BeEquivalentTo(courseTypesList);
        // }

        // [Fact]
        // public void NumberOfStudents_Called_ExpectCountOfStudentsReturned()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var students = new List<Student>
        //     {
        //         new Student("name", 20, "major"),
        //         new Student("name", 40, "major"),
        //     };
        //     var course = new Course("1", "title", 2, CourseType.Lab, students);
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //
        //     var results = courseViewModel.NumberOfStudents;
        //
        //     results.Should().Be(2);
        // }

        // [Fact]
        // public void OldestStudent_Called_ExpectReturnsTheOldestStudentName()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var youngest = new Student("joe", 20, "major");
        //     var oldest = new Student("sam", 40, "major");
        //     var students = new List<Student> { youngest, oldest };
        //     var course = new Course("1", "title", 2, CourseType.Lab, students);
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //
        //     var results = courseViewModel.OldestStudent;
        //
        //     results.Should().Be(oldest.Name);
        // }

        // [Fact]
        // public void StudentOnPropertyChanged_Called_ExpectAverageAgeUpdate()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
        //     var wasAgeChanged = false;
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //     course.Students.ForEach(student => student.PropertyChanged += (sender, args) =>
        //     {
        //         if (args.PropertyName == nameof(Student.Age)) wasAgeChanged = true;
        //     });
        //
        //     courseViewModel.Students.First().Age = 40;
        //
        //     wasAgeChanged.Should().BeTrue();
        // }

        // [Fact]
        // public void StudentOnPropertyChanged_PropertyNameDoesNotEqualAge_ExpectAverageStudentAgeNotUpdated()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
        //     var wasAgeChanged = false;
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //     course.Students.ForEach(student => student.PropertyChanged += (sender, args) =>
        //     {
        //         if (args.PropertyName == nameof(Student.Age)) wasAgeChanged = true;
        //     });
        //
        //     courseViewModel.Students.First().Name = "Joe";
        //
        //     wasAgeChanged.Should().BeFalse();
        // }

        // [Fact]
        // public void RefreshStudents_ConstructorInitialized_ExpectAListOfStudentVMs()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var student1 = new Student("sally", 20, "Math");
        //     var student2 = new Student("joe", 21, "Science");
        //     var students = new List<Student>()
        //     {
        //         student1,
        //         student2
        //     };
        //     var course = new Course("1", "title", 2, CourseType.Lab, students);
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //     var studentViewModelList = new List<StudentViewModel>
        //     {
        //         new StudentViewModel(student1, courseViewModel),
        //         new StudentViewModel(student2, courseViewModel),
        //     };
        //
        //     courseViewModel.Students.Should().BeEquivalentTo(studentViewModelList);
        // }

        // [Fact]
        // public void RefreshStudents_ConstructorInitializedWithEmptyStudentList_ExpectAEmptyListOfStudentVMs()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var students = new List<Student>();
        //     var course = new Course("1", "title", 2, CourseType.Lab, students);
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //
        //     courseViewModel.Students.Should().BeEmpty();
        // }

        [Fact]
        public void RefreshStudents_CourseStudentsIsNull_ExpectStudentsViewModelEmpty()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService);
            var course = new Course("1", "title", 2, CourseType.Lab);
            var courseViewModel = new CourseViewModel(course, courseCollection);

            courseViewModel.Students.Should().BeNull();
        }

        // [Fact]
        // public void RefreshStudents_StudentsModelIsUpdate_ExpectAnUpdatedListOfVMs()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = new CourseCollection(courseDataService);
        //     var student1 = new Student("sally", 20, "Math");
        //     var student2 = new Student("joe", 21, "Science");
        //     var student3 = new Student("sam", 18, "English");
        //     var students = new List<Student>()
        //     {
        //         student1,
        //         student2
        //     };
        //     var course = new Course("1", "title", 2, CourseType.Lab, students);
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //     var studentViewModelList = new List<StudentViewModel>
        //     {
        //         new StudentViewModel(student1, courseViewModel),
        //         new StudentViewModel(student2, courseViewModel),
        //     };
        //     var studentViewModelListUpdated = new List<StudentViewModel>
        //     {
        //         new StudentViewModel(student1, courseViewModel),
        //         new StudentViewModel(student2, courseViewModel),
        //         new StudentViewModel(student3, courseViewModel),
        //     };
        //     courseViewModel.Students.Should().BeEquivalentTo(studentViewModelList);
        //
        //     // In order to trigger a property change, I have to assign a new list:
        //     course.Students = new List<Student>()
        //     {
        //         student1,
        //         student2, student3
        //     };
        //
        //     courseViewModel.Students.Should().BeEquivalentTo(studentViewModelListUpdated);
        // }

        // [Fact]
        // public void AddCourse_Called_ExpectCallsCorrectMethodWithCorrectParam()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
        //     var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //
        //     courseViewModel.AddCourse();
        //
        //     courseCollection.Received().AddCourse(course);
        // }

        // [Fact]
        // public void EditCourse_Called_ExpectCallsCorrectMethodWithCorrectParam()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
        //     var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //
        //     courseViewModel.EditCourse();
        //
        //     courseCollection.Received().EditCourse(course);
        // }

        // [Fact]
        // public void AddStudent_Called_ExpectCallsCorrectMethodWithCorrectParam()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
        //     var course =
        //         Substitute.ForPartsOf<Course>("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
        //     var student = new Student();
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //
        //     courseViewModel.AddStudent(student);
        //
        //     course.Received().AddStudent(student);
        // }

        // [Fact]
        // public void DeleteStudent_Called_ExpectCallsCorrectMethodWithCorrectParam()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
        //     var course =
        //         Substitute.ForPartsOf<Course>("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
        //     var student = new Student();
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //
        //     courseViewModel.DeleteStudent(student);
        //
        //     course.Received().DeleteStudent(student);
        // }
        //
        // [Fact]
        // public void NewStudent_Called_ExpectAStudentViewModel()
        // {
        //     var courseDataService = Substitute.ForPartsOf<CourseDataService>();
        //     var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
        //     var course = new Course("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
        //     var courseViewModel = new CourseViewModel(course, courseCollection);
        //     var studentViewModel = new StudentViewModel(new Student(), courseViewModel);
        //
        //     var result = courseViewModel.NewStudent();
        //
        //     result.Should().BeEquivalentTo(studentViewModel);
        // }

        [Fact]
        public void DeleteCourse_Called_ExpectCallsCorrectMethodWithCorrectParam()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = Substitute.ForPartsOf<CourseCollection>(courseDataService);
            var course =
                Substitute.ForPartsOf<Course>("1", "title", 2, CourseType.Lab, new List<Student> { new Student() });
            var courseViewModel = new CourseViewModel(course, courseCollection);

            courseViewModel.DeleteCourse();

            courseCollection.Received().DeleteCourse(course);
        }
    }
}