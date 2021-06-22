using System.Collections.Generic;

namespace CoursesApp.Models.Builders
{
    public static class CourseBuilder
    {
        public static List<Course> Build()
        {
            return new List<Course>
            {
                new Course(
                    "Algebra",
                    2,
                    new List<Student>
                    {
                        new Student("Joe", 20, "English"),
                        new Student("Sally", 19, "Math"),
                        new Student("Frank", 18, "Math"),
                    },
                    new List<Teacher>
                    {
                        new Teacher("Jack", 35, 10),
                        new Teacher("Jill", 30, 5)
                    },
                    CourseType.Lecture
                ),
                new Course(
                    "English",
                    1,
                    new List<Student>
                    {
                        new Student("James", 29, "Theology"),
                        new Student("Robert", 17, "Science"),
                        new Student("Phillip", 24, "History"),
                    },
                    new List<Teacher>
                    {
                        new Teacher("Charles", 47, 18),
                        new Teacher("Peter", 30, 4)
                    },
                    CourseType.Lab
                ),
                new Course(
                    "History",
                    3,
                    new List<Student>
                    {
                        new Student("Matt", 19, "Math"),
                        new Student("Greg", 18, "History"),
                        new Student("Deb", 18, "History"),
                    },
                    new List<Teacher>
                    {
                        new Teacher("John", 41, 12),
                        new Teacher("Samuel", 68, 40)
                    },
                    CourseType.Seminar
                ),
            };
        }
    }
}