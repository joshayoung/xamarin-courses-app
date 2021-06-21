using FluentAssertions;
using Xunit;

namespace CoursesApp.Models.Test
{
    public class TeacherTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            string name = "name";
            int age = 40;
            int experience = 20;
            
            var teacher = new Teacher(name, age, experience);

            teacher.Name.Should().Be(name);
            teacher.Age.Should().Be(age);
            teacher.Experience.Should().Be(experience);

        }
        
    }
}