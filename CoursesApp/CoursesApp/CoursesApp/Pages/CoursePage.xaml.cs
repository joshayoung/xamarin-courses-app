using System;
using CoursesApp.Helpers;
using CoursesApp.Pages.modals;
using CoursesApp.ViewModels;
using Xamarin.Forms;

namespace CoursesApp.Pages
{
    public partial class CoursePage : ContentPage
    {
        private readonly CourseCollectionViewModel courseCollectionViewModel;

        public CoursePage(CourseCollectionViewModel courseCollectionViewModel)
        {
            InitializeComponent();
            BindingContext = this.courseCollectionViewModel = courseCollectionViewModel;
            welcome.Children.Add(PageHelper.WelcomeText());
        }

        private void ViewCourseDetails(object sender, EventArgs e)
        {
                var courseViewModel = (CourseViewModel)((BindableObject)sender).BindingContext;
                Navigation.PushAsync(new CourseDetailsPage(courseViewModel));
        }

        private void AddCourse(object sender, EventArgs e)
        {
                Navigation.PushModalAsync(new AddCourseModal(courseCollectionViewModel.NewCourseViewModel()));
        }

        private void DeleteClass(object sender, EventArgs e)
        {
                var courseViewModel = (CourseViewModel)((BindableObject)sender).BindingContext;
                courseViewModel.DeleteCourse();
        }

        private void EditClass(object sender, EventArgs e)
        {
                var courseViewModel = (CourseViewModel)((BindableObject)sender).BindingContext;
                Navigation.PushModalAsync(new EditCourseModal(courseViewModel));
        }
    }
}