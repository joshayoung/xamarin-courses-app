using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class CourseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Course course;

        public int StudentCount => course.Students.Count;

        public string HighestMajor
        {
            get
            {
                if (course.Students.Count < 1)
                {
                    return null;
                }

                Dictionary<string, int> counts = new Dictionary<string, int>();
                foreach (var student in course.Students)
                {
                    if (!counts.ContainsKey(student.Major))
                    {
                        counts.Add(student.Major, 1);
                    }
                    else
                    {
                        counts[student.Major]++;
                    }
                }

                var sorted = counts.OrderBy(v => v.Value).ToDictionary(v => v.Key, v => v.Value);

                return sorted.Last().Key;
            }
        }

        public string AverageAgeString => "Average Student Age: " + AverageStudentAge();

        private int AverageStudentAge()
        {
            if (course.Students.Count < 1) return 0;

            int sum = 0;
            course.Students.ForEach(student => sum += student.Age);

            return sum / course.Students.Count;
        }

        public int AverageTeacherAge
        {
            get
            {
                if (course.Teachers.Count < 1) return 0;

                int sum = 0;
                course.Teachers.ForEach(teacher => sum += teacher.Age);

                return sum / course.Teachers.Count;
            }
        }

        public string Title
        {
            get => course.Title;
            set
            {
                course.Title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        public float Length
        {
            get => course.Length;
            set
            {
                course.Length = value;
                NotifyPropertyChanged(nameof(Length));
            }
        }

        private List<StudentViewModel> students;

        public List<StudentViewModel> Students
        {
            get
            {
                if (students == null)
                {
                    students = new List<StudentViewModel>();
                }

                foreach (var student in course.Students)
                {
                    students.Add(new StudentViewModel(student));
                }

                return students;
            }
            set
            {
                students = value;
                NotifyPropertyChanged(nameof(Students));
            }
        }

        private List<TeacherViewModel> teachers;

        public List<TeacherViewModel> Teachers
        {
            get
            {
                if (teachers == null)
                {
                    teachers = new List<TeacherViewModel>();
                }

                foreach (var teacher in course.Teachers)
                {
                    teachers.Add(new TeacherViewModel(teacher));
                }

                return teachers;
            }
            set
            {
                teachers = value;
                NotifyPropertyChanged(nameof(Teacher));
            }
        }

        public CourseType Type
        {
            get => course.Type;
            set
            {
                course.Type = value;
                NotifyPropertyChanged(nameof(Type));
            }
        }

        public CourseViewModel(Course course)
        {
            this.course = course;

            // Update my model's state:
            course.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}