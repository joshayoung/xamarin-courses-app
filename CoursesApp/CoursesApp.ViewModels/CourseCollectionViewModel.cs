using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class CourseCollectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly CourseCollection courseCollection;

        private bool isRefreshing;
        public virtual bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        private List<CourseViewModel> courses;
        public List<CourseViewModel> Courses
        {
            get => courses;
            set
            {
                courses = value;
                OnPropertyChanged();
            }
        }
        
        public CourseCollectionViewModel(CourseCollection courseCollection)
        {
            courses = new List<CourseViewModel>();
            this.courseCollection = courseCollection;
            courseCollection.PropertyChanged += CoursesCollectionOnPropertyChanged;
            RefreshCourses();
            courseCollection.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }

        private void CoursesCollectionOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CourseCollection.Courses)) RefreshCourses();
        }
        
        public int GetNextId => courseCollection.GetNextCourseId();
        
        public bool CoursesExist => courseCollection.CoursesExist;
        
        public void Refresh()
        {
            IsRefreshing = true;
            courseCollection.RepopulateCourseList();
            IsRefreshing = false;
        }

        public CourseViewModel NewCourseViewModel()
        {
            return new CourseViewModel(new Course(courseCollection.GetNextCourseId()), courseCollection);
        }

        private void RefreshCourses()
        {
            var courseList = new List<CourseViewModel>();
            courseCollection.Courses.ForEach(cs =>
            {
                List<StudentViewModel> studentList = cs.Students.Select(id => StudentVm(id, cs)).ToList();
                courseList.Add(CourseVm(cs, studentList));
            });
            Courses = courseList;
            courseCollection.UpdateCoursesExist();
        }

        private CourseViewModel CourseVm(Course cs, List<StudentViewModel> studentList)
        {
            return new CourseViewModel(cs, courseCollection) { Students = studentList };
        }

        private StudentViewModel StudentVm(int id, Course cs)
        {
            return new StudentViewModel(courseCollection.GetStudent(id), cs, courseCollection);
        }
        
        private void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}