using FluentAssertions;
using Xunit;

namespace CoursesApp.Models.Test
{
    public class StudentTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            string name = "name";
            int age = 20;
            string major = "major";
            
            var student = new Student(name, age, major);

            student.Name.Should().Be(name);
            student.Age.Should().Be(age);
            student.Major.Should().Be(major);
        }
        
    }
}