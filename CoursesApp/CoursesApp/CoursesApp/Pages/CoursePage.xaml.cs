using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        private CourseViewModel courseViewModel;
        
        public CoursePage(CourseViewModel courseViewModel)
        {
            InitializeComponent();

            BindingContext = this.courseViewModel = courseViewModel;
        }
    }
}