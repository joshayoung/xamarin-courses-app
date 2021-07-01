using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class CourseCollectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CourseViewModel LastCourse => FindLastCourse();

        public ObservableCollection<CourseViewModel> CoursesWithMultipleStudents { get; } =
            new ObservableCollection<CourseViewModel>();
        
        private readonly CourseCollection coursesCollection;

        public CourseCollectionViewModel(CourseCollection coursesCollection)
        {
            this.coursesCollection = coursesCollection;
            
            coursesCollection.Courses.CollectionChanged += CoursesOnCollectionChanged;
        }

        private void CoursesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastCourse)));
                RefreshMultiStudentCourses();
            }
        }

        private void RefreshMultiStudentCourses()
        {
            CoursesWithMultipleStudents.Clear();
            coursesCollection.Courses.ToList().FindAll(course => course.Students.Count > 1).ToList()
                .ForEach(rec => CoursesWithMultipleStudents.Add(new CourseViewModel(rec)));
        }

        private CourseViewModel FindLastCourse() =>
            (coursesCollection.Courses.Count > 0) ? new CourseViewModel(coursesCollection.Courses.Last()) : null;
        
        public void ReloadTheClasses() => coursesCollection.RepopulateCourseList();
    }
}