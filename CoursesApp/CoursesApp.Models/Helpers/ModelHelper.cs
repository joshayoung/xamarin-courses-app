using System.Collections.Generic;

namespace CoursesApp.Models.Helpers
{
    public static class ModelHelper
    {
        public static List<int> StudentAges()
        {
            return new List<int>
            {
                17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30
            };
        }
        
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