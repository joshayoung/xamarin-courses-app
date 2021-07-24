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
        private readonly CourseCollection courseCollection;

        public int StudentCount => course.Students.Count;

        public string OldestStudent
        {
            get
            {
                var oldest = students.First().Name;
                var oldestAge = students.First().Age;
                foreach (var student in course.Students)
                {
                    if (student.Age > oldestAge)
                    {
                        oldest = student.Name;
                    }
                }

                return oldest;
            }
        }

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
        public string AverageStudentValue => AverageStudentAge().ToString();
        private int AverageStudentAge()
        {
            if (course.Students.Count < 1) return 0;

            int sum = 0;
            course.Students.ForEach(student => sum += student.Age);

            return sum / course.Students.Count;
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
            get => students;
            set
            {
                students = value;
                NotifyPropertyChanged(nameof(Students));
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

        public CourseViewModel(Course course, CourseCollection courseCollection)
        {
            this.course = course;
            this.courseCollection = courseCollection;
            Students = new List<StudentViewModel>();
            
            course.Students.ForEach(student => Students.Add(new StudentViewModel(student)));

            // Update my model's state:
            course.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void AddCourse()
        {
            courseCollection.AddCourse(course);
        }
    }
}