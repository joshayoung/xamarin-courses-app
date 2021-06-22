using System.Collections.Generic;
using System.ComponentModel;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class CourseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Course course;

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