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
        
        [Fact]
        public void Name_PropertyChanged_ExpectPropertyChangedEvent()
        {
            string name = "name";
            int age = 40;
            int experience = 20;
            bool wasChanged = false;
            
            var teacher = new Teacher(name, age, experience);

            teacher.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(teacher.Name))
                {
                    wasChanged = true;
                }
            };

            teacher.Name = "new name";
            wasChanged.Should().BeTrue();
        }
        
        [Fact]
        public void Age_PropertyChanged_ExpectPropertyChangedEvent()
        {
            string name = "name";
            int age = 40;
            int experience = 20;
            bool wasChanged = false;
            
            var teacher = new Teacher(name, age, experience);

            teacher.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(teacher.Age))
                {
                    wasChanged = true;
                }
            };

            teacher.Age = 45;
            wasChanged.Should().BeTrue();
        }
        
        [Fact]
        public void Experience_PropertyChanged_ExpectPropertyChangedEvent()
        {
            string name = "name";
            int age = 40;
            int experience = 20;
            bool wasChanged = false;
            
            var teacher = new Teacher(name, age, experience);

            teacher.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(teacher.Experience))
                {
                    wasChanged = true;
                }
            };

            teacher.Experience = 30;
            wasChanged.Should().BeTrue();
        }
    }
}