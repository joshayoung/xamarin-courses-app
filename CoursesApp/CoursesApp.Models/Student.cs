namespace CoursesApp.Models
{
    public class Student
    {
        public string Name;
        public int Age;
        public string Major;

        public Student(string name, int age, string major)
        {
            Name = name;
            Age = age;
            Major = major;
        }
    }
}