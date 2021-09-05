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

        public int Id => course.Id;

        public string Title
        {
            get => course.Title;
            set => course.Title = value;
        }

        public float Length
        {
            get => course.Length;
            set => course.Length = value;
        }

        public CourseType Type
        {
            get => course.Type;
            set => course.Type = value;
        }
        
        private List<StudentViewModel> students;
        public List<StudentViewModel> Students
        {
            get => students;
            set
            {
                students = value;
                // NOTE: Refreshes property, I think this is what causes the list to refresh after RefreshStudents():
                NotifyPropertyChanged();
                course.UpdateAverageAge(courseCollection);
                course.UpdateOldestStudent(courseCollection);
                course.UpdateStudentCount();
                course.UpdateStudentsExist();
            }
        }
        
        public int AverageStudentAge => course.AverageStudentAge;
        
        public string OldestStudent => course.OldestStudent;

        public int NumberOfStudents => course.NumberOfStudents;
        
        public bool StudentsExist => course.StudentsExist;
        
        public static List<float> Lengths => ModelHelper.CourseLengths;
        
        public static List<CourseType> Types => ModelHelper.CourseTypes;

        public CourseViewModel(Course course, CourseCollection courseCollection)
        {
            this.course = course;
            this.courseCollection = courseCollection;
            students = new List<StudentViewModel>();
            RefreshStudents();
            courseCollection.PropertyChanged += StudentsCollectionOnPropertyChanged;
            
            // NOTE: If my model value changes, update the vm value
            course.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(Course.Title):
                        NotifyPropertyChanged(nameof(Title));
                        break;
                    case nameof(Course.Length):
                        NotifyPropertyChanged(nameof(Length));
                        break;
                    case nameof(Course.Type):
                        NotifyPropertyChanged(nameof(Type));
                        break;
                    case nameof(Course.AverageStudentAge):
                        NotifyPropertyChanged(nameof(AverageStudentAge));
                        break;
                    case nameof(Course.OldestStudent):
                        NotifyPropertyChanged(nameof(OldestStudent));
                        break;
                    case nameof(Course.NumberOfStudents):
                        NotifyPropertyChanged(nameof(NumberOfStudents));
                        break;
                    case nameof(Course.StudentsExist):
                        NotifyPropertyChanged(nameof(StudentsExist));
                        break;
                }
            };
        }

        private void StudentsCollectionOnPropertyChanged(object _, PropertyChangedEventArgs e)
        {
            // NOTE: Refresh list after deleting or adding a student
            if (e.PropertyName == nameof(CourseCollection.Students)) RefreshStudents();
        }
        
        public void AddCourse() => courseCollection.AddCourse(course);

        public StudentViewModel NewStudent()
        {
            return new StudentViewModel(new Student(GetNextCourseId()), course, courseCollection);
        }

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

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}