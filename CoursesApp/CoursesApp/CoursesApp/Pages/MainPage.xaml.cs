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
            try
            {
                var courseViewModel = (CourseViewModel)((BindableObject)sender).BindingContext;
                Navigation.PushAsync(new CoursePage(courseViewModel));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void AddCourse(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushModalAsync(new AddCoursePage(courseCollectionViewModel.NewCourseViewModel()));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void DeleteClass(object sender, EventArgs e)
        {
            try
            {
                var courseViewModel = (CourseViewModel)((BindableObject)sender).BindingContext;
                courseViewModel.DeleteCourse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void EditClass(object sender, EventArgs e)
        {
            try
            {
                var courseViewModel = (CourseViewModel)((BindableObject)sender).BindingContext;
                Navigation.PushModalAsync(new EditCoursePage(courseViewModel));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}