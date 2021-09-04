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

        public List<CourseViewModel> Courses { get; set; }
    
        public CourseCollectionViewModel(CourseCollection courseCollection)
        {
            Courses = new List<CourseViewModel>();
            this.courseCollection = courseCollection;
            courseCollection.PropertyChanged += CoursesCollectionOnPropertyChanged;
            RefreshCourses();
            
            // Update the VM's Courses if the Models Courses changes:
            // This could also be done with a longer get/set and using 'OnPropertyChanged();':
            courseCollection.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CourseCollection.Courses))
                {
                    PropertyChanged?.Invoke(this, args);
                }
            };
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