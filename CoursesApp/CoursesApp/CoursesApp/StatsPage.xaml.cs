using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatsPage : ContentPage
    {
        private CourseViewModel courseViewModel;
        
        public StatsPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();

            BindingContext = this.courseViewModel = courseViewModel;
        }

        private void ViewStudents(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CoursePage(courseViewModel));
        }

        private void ViewTeachers(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CoursePage(courseViewModel));
        }
    }
}