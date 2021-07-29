using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCoursePage : ContentPage
    {
        private readonly CourseViewModel courseViewModel;

        public AddCoursePage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            BindingContext = this.courseViewModel = courseViewModel;
        }

        private void SaveCourse(object sender, EventArgs e)
        {
            courseViewModel.AddCourse();
            Navigation.PopAsync();
        }
    }
}