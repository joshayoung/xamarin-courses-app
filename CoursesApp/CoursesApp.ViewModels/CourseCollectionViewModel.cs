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
        
        public readonly CourseCollection CoursesCollection;

        public CourseCollectionViewModel(CourseCollection coursesCollection)
        {
            CoursesCollection = coursesCollection;
            
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
            CoursesCollection.Courses.ToList().FindAll(course => course.Students.Count > 1).ToList()
                .ForEach(rec => CoursesWithMultipleStudents.Add(new CourseViewModel(rec)));
        }

        private CourseViewModel FindLastCourse() =>
            (CoursesCollection.Courses.Count > 0) ? new CourseViewModel(CoursesCollection.Courses.Last()) : null;
        
        public void ReloadTheClasses() => CoursesCollection.RepopulateCourseList();
    }
}