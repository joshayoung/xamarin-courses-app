using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoursesApp.Models;
using Xamarin.Forms;

namespace CoursesApp.ViewModels
{
    public class CourseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Course course;

        public int StudentCount => course.Students.Count;

        private string highestMajor;
        public string HighestMajor
        {
            get
            {
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

                //counts.ToList().Sort((val1, val2) => val1.Value.CompareTo(val2.Value));

                var sorted = counts.OrderBy(v => v.Value).ToDictionary(v => v.Key, v => v.Value);

                return sorted.Last().Key;
            }
            set => highestMajor = value;
        }

        public string AverageStudentAgeWithText => AverageAgeString();

        public string AverageAgeString()
        {
            return "Average Student Age: " + AverageStudentAge.ToString();
            Label myLabel = new Label
            {
                BackgroundColor = Color.Red,
                FontSize = 24
            };
        }

        private int averageStudentAge;
        public int AverageStudentAge
        {
            get
            {
                int sum = 0;
                foreach (var student in course.Students)
                {
                    sum += student.Age;
                }

                return sum / course.Students.Count;
            }
            set
            {
                averageStudentAge = value;
            }
        }
        
        private int averageTeacherAge;
        public int AverageTeacherAge
        {
            get
            {
                int sum = 0;
                foreach (var teacher in course.Teachers)
                {
                    sum += teacher.Age;
                }

                return sum / course.Teachers.Count;
            }
            set
            {
                averageTeacherAge = value;
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