using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoursesApp.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CourseType
    {
        [Description("Seminar Class")] Seminar,
        [Description("Lab Class")] Lab,
        [Description("Independent Study Class")] Independent,
        [Description("Lecture Class")] Lecture,
        [Description("Discussion Class")] Discussion,
        [Description("Default for New Items")] None
    }
}