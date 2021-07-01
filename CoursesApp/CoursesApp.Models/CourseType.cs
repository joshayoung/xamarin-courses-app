using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoursesApp.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CourseType
    {
        Seminar,
        Lab,
        IndependentStudy,
        Lecture,
        Discussion
    }
}