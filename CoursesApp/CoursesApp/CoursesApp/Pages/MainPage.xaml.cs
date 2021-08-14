using System;
using CoursesApp.Helpers;
using CoursesApp.ViewModels;
using Xamarin.Forms;

namespace CoursesApp.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly CourseCollectionViewModel courseCollectionViewModel;

        public MainPage(CourseCollectionViewModel courseCollectionViewModel)
        {
            InitializeComponent();
            BindingContext = this.courseCollectionViewModel = courseCollectionViewModel;
            welcome.Children.Add(PageHelper.WelcomeText());
        }

        private void ViewCourseDetails(object sender, EventArgs e)
        {
                var courseViewModel = (CourseViewModel)((BindableObject)sender).BindingContext;
                Navigation.PushAsync(new CoursePage(courseViewModel));
        }

        private void AddCourse(object sender, EventArgs e)
        {
                Navigation.PushModalAsync(new AddCoursePage(courseCollectionViewModel.NewCourseViewModel()));
        }

        private void DeleteClass(object sender, EventArgs e)
        {
                var courseViewModel = (CourseViewModel)((BindableObject)sender).BindingContext;
                courseViewModel.DeleteCourse();
        }

        private void EditClass(object sender, EventArgs e)
        {
                var courseViewModel = (CourseViewModel)((BindableObject)sender).BindingContext;
                Navigation.PushModalAsync(new EditCoursePage(courseViewModel));
        }
    }
}