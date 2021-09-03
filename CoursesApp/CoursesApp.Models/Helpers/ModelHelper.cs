using System.Collections.Generic;
using System.Linq;

namespace CoursesApp.Models.Helpers
{
    public static class ModelHelper
    {
        public static List<int> StudentAges() => Enumerable.Range(17, 40 - 17).ToList();
        
        public static List<CourseType> CourseTypes =>
            new List<CourseType>
            {
                CourseType.Seminar,
                CourseType.Lab,
                CourseType.Independent,
                CourseType.Lecture,
                CourseType.Discussion
            };
        
        public static List<float> CourseLengths => new List<float> { 1, 2, 3, 4 };
    }
}