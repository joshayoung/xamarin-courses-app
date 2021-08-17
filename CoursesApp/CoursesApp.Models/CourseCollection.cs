using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoursesApp.Models.Service;

namespace CoursesApp.Models
{
    public class CourseCollection : INotifyPropertyChanged
    {
        private readonly CourseDataService courseDataService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<Course> Courses { get; set; } = new List<Course>();

        public List<Student> Students { get; set; } = new List<Student>();

        public CourseCollection(CourseDataService courseDataService)
        {
            this.courseDataService = courseDataService;
        }

        public void RepopulateCourseList()
        {
            Courses = courseDataService.GetCourses();
            Students = courseDataService.GetStudents();
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Courses)));
        }

        public void EditCourse(Course course)
        {
            // Right now, all of the local changes are handled through Pub/Sub.
            // This is where I could call out to my API to save this record in the DB.
        }

        public virtual void DeleteCourse(Course course)
        {
            // TODO: Also remove student if only in this course
            Courses.Remove(course);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Courses)));
        }

        public void AddStudent(Course course, Student student)
        {
            Students.Add(student);
            course.Students.Add(student.Id);
            
            // Trigger a Change for Both Lists:
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Courses)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Students)));
        }

        public void DeleteStudent(Course course, Student student)
        {
            var students = Courses.Where(c => c.Students.Contains(student.Id)).ToList();
            // If student not in multiple courses:
            if (students.Count == 1)
            {
                Students.Remove(student);
            }
            course.Students.Remove(student.Id);
            
            // Trigger a Change for Both Lists:
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Courses)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Students)));
        }
    }
}