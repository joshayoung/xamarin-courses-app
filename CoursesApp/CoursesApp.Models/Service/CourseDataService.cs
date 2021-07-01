using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace CoursesApp.Models.Service
{
    public static class CourseDataService
    {
        private const string DataPath = "CoursesApp.Models.Service.courses.json";

        public static List<Course> GetCourses()
        {
            var json = GetJsonString();
            return JsonConvert.DeserializeObject<List<Course>>(json);
        }

        private static string GetJsonString()
        {
            var assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(DataPath);
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

    }
}