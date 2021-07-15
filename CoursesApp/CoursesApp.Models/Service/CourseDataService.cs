using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace CoursesApp.Models.Service
{
    public class CourseDataService
    {
        private const string DataPath = "CoursesApp.Models.Service.courses.json";

        public virtual List<Course> GetCourses(string path = DataPath)
        {
            var json = GetJsonString(path);
            return JsonConvert.DeserializeObject<List<Course>>(json);
        }

        private static string GetJsonString(string json)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(json);
            if (stream == null) throw new InvalidOperationException("Error reading JSON file");

            return new StreamReader(stream).ReadToEnd();
        }
    }
}