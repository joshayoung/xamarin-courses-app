using FluentAssertions;
using Xunit;

namespace CoursesApp.Models.Test
{
    public class StudentTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            var name = "name";
            var age = 20;
            var major = "major";
            
            var student = new Student(name, age, major);

            student.Name.Should().Be(name);
            student.Age.Should().Be(age);
            student.Major.Should().Be(major);
        }
        
        [Fact]
        public void Name_PropertyChanged_ExpectPropertyChangedEvent()
        {
            var name = "name";
            var age = 20;
            var major = "major";
            var wasChanged = false;
            
            var student = new Student(name, age, major);

            student.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(student.Name))
                {
                    wasChanged = true;
                }
            };

            student.Name = "new name";
            wasChanged.Should().BeTrue();
        }
        
        [Fact]
        public void Age_PropertyChanged_ExpectPropertyChangedEvent()
        {
            var name = "name";
            var age = 20;
            var major = "major";
            var wasChanged = false;
            
            var student = new Student(name, age, major);

            student.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(student.Age))
                {
                    wasChanged = true;
                }
            };

            student.Age = 24;
            wasChanged.Should().BeTrue();
        }
        
        [Fact]
        public void Major_PropertyChanged_ExpectPropertyChangedEvent()
        {
            var name = "name";
            var age = 20;
            var major = "major";
            var wasChanged = false;
            
            var student = new Student(name, age, major);

            student.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(student.Major))
                {
                    wasChanged = true;
                }
            };

            student.Major = "new major";
            wasChanged.Should().BeTrue();
        }
    }
}