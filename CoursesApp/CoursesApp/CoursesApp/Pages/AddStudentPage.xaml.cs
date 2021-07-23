using System;
using CoursesApp.Models;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddStudentPage : ContentPage
    {
        private readonly CourseViewModel courseViewModel;
        
        public AddStudentPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            this.courseViewModel = courseViewModel;

            BindingContext = new StudentViewModel(new Student("", 0, ""));
        }

        private void SaveStudent(object sender, EventArgs e)
        {
            var studentViewModel = (StudentViewModel)((BindableObject) sender).BindingContext;
            courseViewModel.AddStudent(studentViewModel);
            Navigation.PopAsync();
        }
    }
}