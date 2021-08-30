using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CoursesApp.Models;
using CoursesApp.Models.Helpers;

namespace CoursesApp.ViewModels
{
    public class CourseViewModel : INotifyPropertyChanged
    {
        private readonly Course course;
        private readonly CourseCollection courseCollection;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool StudentsExist => course.StudentsExist;

        public int NumberOfStudents => course.NumberOfStudents;

        public int AverageStudentAge => course.AverageStudentAge;

        public string? OldestStudent => course.OldestStudent;

        public int Id => course.Id;

        public string Title
        {
            get => course.Title;
            set
            {
                course.Title = value;
                NotifyPropertyChanged();
            }
        }

        public float Length
        {
            get => course.Length;
            set
            {
                course.Length = value;
                NotifyPropertyChanged();
            }
        }
        
        public static List<float> Lengths => ModelHelper.CourseLengths;

        private List<StudentViewModel> students;

        public List<StudentViewModel> Students
        {
            get => students;
            set
            {
                students = value;
                NotifyPropertyChanged();
                course.UpdateAverageAge(courseCollection);
                course.UpdateOldestStudent(courseCollection);
                course.UpdateStudentCount();
                course.UpdateStudentsExist();
            }
        }

        public CourseType Type
        {
            get => course.Type;
            set
            {
                course.Type = value;
                NotifyPropertyChanged();
            }
        }
        
        public static List<CourseType> Types => ModelHelper.CourseTypes;

        public CourseViewModel(Course course, CourseCollection courseCollection)
        {
            students = new List<StudentViewModel>();
            this.course = course;
            this.courseCollection = courseCollection;
            RefreshStudents();
            courseCollection.PropertyChanged += StudentsCollectionOnPropertyChanged;
            course.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }

        public void AddCourse() => courseCollection.AddCourse(course);

        public StudentViewModel NewStudent() =>
            new StudentViewModel(new Student(GetNextCourseId()), course, courseCollection);

        public void DeleteCourse() => courseCollection.DeleteCourse(course);
        
        public CourseViewModel EditCourseCopy(int id)
        {
            var newCourse = new Course(id, course.Title ?? "", course.Length, course.Type);
            return new CourseViewModel(newCourse, courseCollection);
        }

        public void SaveCourse(int id)
        {
            Course editCourse = courseCollection.Courses.First(c => c.Id == id);
            editCourse.Title = course.Title;
            editCourse.Length = course.Length;
            editCourse.Type = course.Type;
        }
        
        private int GetNextCourseId() => courseCollection.Students.Max(student => student.Id) + 1;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void StudentsCollectionOnPropertyChanged(object _, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CourseCollection.Students)) RefreshStudents();
        }

        private void RefreshStudents()
        {
            // Account for a new class:
            if (!courseCollection.Courses.Contains(course)) return;

            var cs = courseCollection.Courses.First(cs => cs == course);

            IEnumerable<StudentViewModel>
                studentList = cs.Students.Select(student =>
                    new StudentViewModel(courseCollection.GetStudent(student), cs, courseCollection));
            Students = new List<StudentViewModel>(studentList);
        }
    }
}