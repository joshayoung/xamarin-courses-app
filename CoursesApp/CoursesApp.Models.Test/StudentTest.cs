using FluentAssertions;
using Xunit;

namespace CoursesApp.Models.Test
{
    public class StudentTest
    {
        [Fact]
        public void Constructor_DefaultParams_ExpectAssignment()
        {
            const int id = 1;
        
            var student = new Student(id);
        
            student.Id.Should().Be(id);
            student.Name.Should().Be("");
            student.Age.Should().Be(0);
            student.Major.Should().Be("");
        }
        
        [Fact]
        public void Constructor_NonDefaultParams_ExpectAssignment()
        {
            const int id = 1;
            const string name = "name";
            const int age = 20;
            const string major = "major";
        
            var student = new Student(id, name, age, major);
        
            student.Id.Should().Be(id);
            student.Name.Should().Be(name);
            student.Age.Should().Be(age);
            student.Major.Should().Be(major);
        }
        
        [Fact]
        public void Properties_Changed_ExpectPropertyChangedEvent()
        {
            var wasNameChanged = false;
            var wasAgeChanged = false;
            var wasMajorChanged = false;
            var student = new Student(1, "name", 20, "major");
            student.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(student.Name)) wasNameChanged = true;
                if (args.PropertyName == nameof(student.Age)) wasAgeChanged = true;
                if (args.PropertyName == nameof(student.Major)) wasMajorChanged = true;
            };
        
            student.Name = "new name";
            student.Age = 21;
            student.Major = "new major";
        
            wasNameChanged.Should().BeTrue();
            wasAgeChanged.Should().BeTrue();
            wasMajorChanged.Should().BeTrue();
        }
    }
}