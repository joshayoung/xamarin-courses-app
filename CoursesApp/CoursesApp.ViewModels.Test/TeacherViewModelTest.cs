using CoursesApp.Models;
using FluentAssertions;
using Xunit;

namespace CoursesApp.ViewModels.Test
{
    public class TeacherViewModelTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            string name = "Pete";
            int age = 30;
            int experience = 20;
            var teacher = new Teacher(name, age, experience);

            var teacherViewModel = new TeacherViewModel(teacher);

            teacherViewModel.Name.Should().Be(teacher.Name);
            teacherViewModel.Age.Should().Be(teacher.Age);
            teacherViewModel.Experience.Should().Be(teacher.Experience);
        }

        [Fact]
        public void ViewModel_PropertyChanged_ExpectPropertyChangedEvents()
        {
            string name = "Joe";
            int age = 31;
            int experience = 20;
            var teacher = new Teacher(name, age, experience);
            var teacherViewModel = new TeacherViewModel(teacher);
            var nameWasChanged = false;
            var ageWasChanged = false;
            var experienceWasChanged = false;

            teacherViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(teacherViewModel.Name)) nameWasChanged = true;
                if (args.PropertyName == nameof(teacherViewModel.Age)) ageWasChanged = true;
                if (args.PropertyName == nameof(teacherViewModel.Experience)) experienceWasChanged = true;
            };
            
            teacherViewModel.Name = "new name";
            teacherViewModel.Age = 38;
            teacherViewModel.Experience = 19;

            nameWasChanged.Should().BeTrue();
            ageWasChanged.Should().BeTrue();
            experienceWasChanged.Should().BeTrue();
        }

        [Fact]
        public void Model_PropertyChanged_ExpectPropertyChangedEvent()
        {
            string name = "Joe";
            int age = 31;
            int experience = 20;
            var teacher = new Teacher(name, age, experience);
            var teacherViewModel = new TeacherViewModel(teacher);
            var wasChanged = false;

            teacherViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(teacherViewModel.Experience)) wasChanged = true;
            };

            teacher.Experience = 2;
            wasChanged.Should().BeTrue();
        }
    }
}