namespace CoursesApp.Models
{
    public class Teacher
    {
        public string Name;
        public int Age;
        public int Experience;

        public Teacher(string name, int age, int experience)
        {
            Name = name;
            Age = age;
            Experience = experience;
        }
    }
}