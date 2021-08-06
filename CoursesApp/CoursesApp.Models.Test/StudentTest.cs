using FluentAssertions;
using Xunit;

namespace CoursesApp.Models.Test
{
    public class StudentTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            var id = "123";
            var name = "name";
            var age = 20;
            var major = "major";

            var student = new Student(name, age, major);

            // TODO: Should this be null?
            student.Id.Should().BeNull();
            student.Name.Should().Be(name);
            student.Age.Should().Be(age);
            student.Major.Should().Be(major);
        }

        [Fact]
        public void Properties_Changed_ExpectPropertyChangedEvent()
        {
            var wasIdChanged = false;
            var wasNameChanged = false;
            var wasAgeChanged = false;
            var wasMajorChanged = false;
            var student = new Student("name", 20, "major");
            student.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(student.Id)) wasIdChanged = true;
                if (args.PropertyName == nameof(student.Name)) wasNameChanged = true;
                if (args.PropertyName == nameof(student.Age)) wasAgeChanged = true;
                if (args.PropertyName == nameof(student.Major)) wasMajorChanged = true;
            };

            student.Id = "123";
            student.Name = "new name";
            student.Age = 21;
            student.Major = "new major";

            wasIdChanged.Should().BeTrue();
            wasNameChanged.Should().BeTrue();
            wasAgeChanged.Should().BeTrue();
            wasMajorChanged.Should().BeTrue();
        }
    }
}