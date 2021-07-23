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
        public StudentViewModel StudentViewModel = new StudentViewModel(new Student("", 0, ""));
        
        public AddStudentPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            this.courseViewModel = courseViewModel;

            BindingContext = StudentViewModel;
        }

        private void SaveStudent(object sender, EventArgs e)
        {
            var studentViewModel = (StudentViewModel)((BindableObject) sender).BindingContext;
            courseViewModel.Students.Add(studentViewModel);
            Navigation.PopAsync();
        }
    }
}