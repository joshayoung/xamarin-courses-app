using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CoursesApp.Models.Service;

namespace CoursesApp.Models
{
    public class CourseCollection : INotifyPropertyChanged
    {
        private readonly CourseDataService courseDataService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<Course> Courses { get; private set; } = new List<Course>();

        public List<Student> Students { get; private set; } = new List<Student>();
        
        public bool CoursesExist { get; private set; }

        public CourseCollection(CourseDataService courseDataService)
        {
            this.courseDataService = courseDataService;
        }

        public void RepopulateCourseList()
        {
            Courses = courseDataService.GetCourses();
            Students = courseDataService.GetStudents();
            OnPropertyChanged(nameof(Courses));
            OnPropertyChanged(nameof(Students));
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
            OnPropertyChanged(nameof(Courses));
        }

        public virtual void DeleteCourse(Course course)
        {
            Courses.Remove(course);
            OnPropertyChanged(nameof(Courses));
        }
        
        public int GetNextCourseId()
        {
            if (Courses.Count == 0) return 1;

            var id = Courses.Max(course => course.Id);

            return ++id;
        }

        public void UpdateCoursesExist()
        {
            CoursesExist = Courses.Count > 0;
            OnPropertyChanged(nameof(CoursesExist));
        }

        public void AddStudent(Course course, Student student)
        {
            Students.Add(student);
            course.Students.Add(student.Id);
            OnPropertyChanged(nameof(Students));
        }
        
        public void DeleteStudent(Course course, Student student)
        {
            var coursesThatHaveStudent = Courses.Where(c => c.Students.Contains(student.Id)).ToList();
            // If student not in multiple courses:
            if (coursesThatHaveStudent.Count == 1) Students.Remove(student);
            course.Students.Remove(student.Id);
            OnPropertyChanged(nameof(Students));
        }
        
        public Student GetStudent(int id) => Students.Find(student => student.Id == id);
        
        private void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}