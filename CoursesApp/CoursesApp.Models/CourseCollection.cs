using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CoursesApp.Models.Service;

namespace CoursesApp.Models
{
    public class CourseCollection : INotifyPropertyChanged
    {
        private readonly CourseDataService courseDataService;


        private List<Course> courses;

        public List<Course> Courses
        {
            get => courses;
            private set
            {
                courses = value ?? new List<Course>();
                OnPropertyChanged();
            }
        }

        private List<Student> students;

        public List<Student> Students
        {
            get => students;
            private set
            {
                students = value ?? new List<Student>();
                OnPropertyChanged();
            }
        }

        public CourseCollection(CourseDataService courseDataService)
        {
            this.courseDataService = courseDataService;
        }

        public void RepopulateCourseList()
        {
            Courses = courseDataService.GetCourses();
            Students = courseDataService.GetStudents();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Courses)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Students)));
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Courses)));
        }

        public void EditCourse(Course course)
        {
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
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}