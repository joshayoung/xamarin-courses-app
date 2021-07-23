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
        private readonly StudentViewModel studentViewModel;
        
        public AddStudentPage(CourseViewModel courseViewModel)
        //public AddStudentPage(StudentViewModel studentViewModel)
        {
            this.courseViewModel = courseViewModel;
            InitializeComponent();
            BindingContext = new Student("", 1, "");
        }

        private void SaveStudent(object sender, EventArgs e)
        {
            var student = (Student)((BindableObject) sender).BindingContext;
            courseViewModel.AddStudent(student);
            Navigation.PopAsync();
        }
    }
}