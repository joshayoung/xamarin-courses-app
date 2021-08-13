using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace CoursesApp.Models.Service
{
    public class CourseDataService
    {
        private const string CourseDataPath = "CoursesApp.Models.Service.courses.json";
        private const string StudentDataPath = "CoursesApp.Models.Service.students.json";

        public virtual List<Course> GetCourses(string path = CourseDataPath)
        {
            var json = GetJsonString(path);
            
            return JsonConvert.DeserializeObject<List<Course>>(json);
        }

        public List<Student> GetStudents(string path = StudentDataPath)
        {
            var json = GetJsonString(path);
            
            return JsonConvert.DeserializeObject<List<Student>>(json);
        }

        private static string GetJsonString(string json)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(json);
            if (stream == null) throw new InvalidOperationException("Error reading JSON file");

            return new StreamReader(stream).ReadToEnd();
        }
    }
}