using FluentAssertions;
using Xunit;

namespace CoursesApp.Models.Test
{
    public class StudentTest
    {
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