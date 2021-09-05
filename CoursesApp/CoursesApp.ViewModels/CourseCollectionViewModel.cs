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
                // NOTE: Refresh property when changed (otherwise the pull to refresh will not close spinner):
                NotifyPropertyChanged();
            }
        }

        public List<CourseViewModel> Courses { get; set; }

        // NOTE: I do not need a Students property here, because my students
        // are added to my Courses List with RefreshCourses().
        
        public bool CoursesExist => courseCollection.CoursesExist;
        
        public CourseCollectionViewModel(CourseCollection courseCollection)
        {
            Courses = new List<CourseViewModel>();
            this.courseCollection = courseCollection;
            courseCollection.PropertyChanged += CoursesCollectionOnPropertyChanged;
            RefreshCourses();
            
            // Update the VM's Courses, CoursesExist if the Models Courses changes:
            courseCollection.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(CourseCollection.Courses):
                        NotifyPropertyChanged(nameof(Courses));
                        break;
                    case nameof(CourseCollection.CoursesExist):
                        NotifyPropertyChanged(nameof(CoursesExist));
                        break;
                }
            };
        }

        private void CoursesCollectionOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // NOTE: this listens for property changes after deleting or adding a course and refreshes the list:
            if (e.PropertyName == nameof(CourseCollection.Courses)) RefreshCourses();
        }
        
        public int GetNextId => courseCollection.GetNextCourseId();
        
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
        
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}