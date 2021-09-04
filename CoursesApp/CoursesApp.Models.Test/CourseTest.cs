using System.Collections.Generic;
using CoursesApp.Models.Service;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CoursesApp.Models.Test
{
    public class CourseTest
    {
        [Fact]
        public void Constructor_DefaultParams_ExpectAssignment()
        {
            const int id = 1;

            var course = new Course(id);

            course.Id.Should().Be(id);
            course.Title.Should().Be("");
            course.Length.Should().Be(0);
            course.Type.Should().Be(CourseType.None);
            course.Students.Should().BeEmpty();
            course.AverageStudentAge.Should().Be(0);
            course.OldestStudent.Should().Be("");
            course.NumberOfStudents.Should().Be(0);
            course.StudentsExist.Should().BeFalse();
        }
        
        [Fact]
        public void Constructor_NonDefaultParams_ExpectAssignment()
        {
            const int id = 1;
            const string title = "title";
            const float length = 2;
            const CourseType type = CourseType.Lab;
            var students = new List<int> { 1 };

            var course = new Course(id, title, length, type, students);

            course.Id.Should().Be(id);
            course.Title.Should().Be(title);
            course.Length.Should().Be(length);
            course.Type.Should().Be(type);
            course.Students.Should().BeEquivalentTo(students);
        }

        [Fact]
        public void PropertiesChange_Called_ExpectPropertyChangedEvent()
        {
            var wasTitleChanged = false;
            var wasLengthChanged = false;
            var wasTypeChanged = false;
            var wasStudentsChanged = false;
            var course = new Course(1, "title", 2, CourseType.Lab, new List<int>());
            course.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(course.Title)) wasTitleChanged = true;
                if (args.PropertyName == nameof(course.Length)) wasLengthChanged = true;
                if (args.PropertyName == nameof(course.Type)) wasTypeChanged = true;
                if (args.PropertyName == nameof(course.Students)) wasStudentsChanged = true;
            };

            course.Title = "new title";
            course.Length = 4;
            course.Type = CourseType.Discussion;
            course.Students = new List<int> { 1 };
            
            wasTitleChanged.Should().BeTrue();
            wasLengthChanged.Should().BeTrue();
            wasTypeChanged.Should().BeTrue();
            wasStudentsChanged.Should().BeTrue();
        }

        [Fact]
        public void UpdateAverageAge_Called_PropertyChangeForAverageStudentAge()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService) { Students = { new Student(1) } };
            var course = new Course(1, "title", 2, CourseType.Discussion, new List<int>() { 1 });
            courseCollection.Courses.Add(course);
            var wasAverageUpdated = false;
            course.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Course.AverageStudentAge)) wasAverageUpdated = true;
            };

            course.UpdateAverageAge(courseCollection);

            wasAverageUpdated.Should().BeTrue();
        }
        
        [Fact]
        public void UpdateAverageAge_CalledWithZeroStudents_PropertyChangeForAverageStudentAge()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService) { Students = { new Student(1) } };
            var course = new Course(1, "title", 2, CourseType.Discussion);
            courseCollection.Courses.Add(course);
            var wasAverageUpdated = false;
            course.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Course.AverageStudentAge)) wasAverageUpdated = true;
            };

            course.UpdateAverageAge(courseCollection);

            wasAverageUpdated.Should().BeTrue();
        }

        [Fact]
        public void UpdateAverageAge_NoStudents_ExpectAverageStudentAgeSetToZero()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService) { Students = { new Student(1) } };
            var course = new Course(1, "title", 2, CourseType.Discussion);
            courseCollection.Courses.Add(course);

            course.UpdateAverageAge(courseCollection);

            course.AverageStudentAge.Should().Be(0);
        }
        
        [Fact]
        public void UpdateAverageAge_HasStudents_ExpectAverageStudentAgeSet()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService)
            {
                Students = { new Student(1, "joe", 30), 
                             new Student(2, "sally", 20) 
                }
            };
            var course = new Course(1, "title", 2, CourseType.Discussion, new List<int>{ 1, 2 });
            courseCollection.Courses.Add(course);

            course.UpdateAverageAge(courseCollection);

            course.AverageStudentAge.Should().Be(25);
        }

        [Fact]
        public void UpdateStudentCount_Called_ExpectPropertyChangeForNumberOfStudents()
        {
            var course = new Course(1, "title", 2, CourseType.Discussion);
            var wasNumberOfStudentsChanged = false;
            course.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Course.NumberOfStudents)) wasNumberOfStudentsChanged = true;
            };

            course.UpdateStudentCount();

            wasNumberOfStudentsChanged.Should().BeTrue();
        }
        
        [Fact]
        public void UpdateStudentCount_Called_ExpectNumberOfStudentsSet()
        {
            var course = new Course(1, "title", 2, CourseType.Discussion, new List<int>{ 1, 2, 3 });

            course.UpdateStudentCount();

            course.NumberOfStudents.Should().Be(3);
        }
        
        [Fact]
        public void UpdateOldestStudent_Called_PropertyChangeForOldestStudent()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService)
            {
                Students = { new Student(1, "Joe", 30), new Student(2, "Sally", 20) }
            };
            var course = new Course(1, "title", 2, CourseType.Discussion, new List<int>{ 1, 2 });
            courseCollection.Courses.Add(course);
            var wasOldestStudentUpdated = false;
            course.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Course.OldestStudent)) wasOldestStudentUpdated = true;
            };

            course.UpdateOldestStudent(courseCollection);

            wasOldestStudentUpdated.Should().BeTrue();
        }
        
        [Fact]
        public void UpdateOldestStudent_CalledWithZeroStudents_OldestStudentSetToEmptyString()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService)
            {
                Students = { new Student(1, "Joe", 30), new Student(2, "Sally", 20) }
            };
            var course = new Course(1, "title", 2, CourseType.Discussion, null);
            courseCollection.Courses.Add(course);

            course.UpdateOldestStudent(courseCollection);

            course.OldestStudent.Should().Be("");
        }
        
        [Fact]
        public void UpdateOldestStudent_CalledWithStudents_OldestStudentSet()
        {
            var courseDataService = Substitute.ForPartsOf<CourseDataService>();
            var courseCollection = new CourseCollection(courseDataService)
            {
                Students = { new Student(1, "Joe", 30), new Student(2, "Sally", 20) }
            };
            var course = new Course(1, "title", 2, CourseType.Discussion, new List<int>{ 1, 2 });
            courseCollection.Courses.Add(course);

            course.UpdateOldestStudent(courseCollection);

            course.OldestStudent.Should().Be("Joe");
        }

        [Fact]
        public void UpdateStudentsExist_Called_ExpectStudentsExistPropertyChanged()
        {
            var course = new Course(1, "title", 2, CourseType.Discussion);
            var wasStudentsExistUpdated = false;
            course.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Course.StudentsExist)) wasStudentsExistUpdated = true;
            };

            course.UpdateStudentsExist();

            wasStudentsExistUpdated.Should().BeTrue();
        }
        
        [Fact]
        public void UpdateStudentsExist_Called_ExpectStudentsExistUpdated()
        {
            var course = new Course(1, "title", 2, CourseType.Discussion, new List<int> { 1 });

            course.UpdateStudentsExist();

            course.StudentsExist.Should().BeTrue();
        }
    }
}