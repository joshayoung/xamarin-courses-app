using System;
using System.Collections.Generic;
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
        
        public AddStudentPage(StudentViewModel studentViewModel, List<StudentViewModel> students)
        {
            this.studentViewModel = studentViewModel;
            InitializeComponent();
            BindingContext = studentViewModel;
        }

        private void SaveStudent(object sender, EventArgs e)
        {
            var student = (StudentViewModel)((BindableObject) sender).BindingContext;
            //courseViewModel.AddStudent(student);
            Navigation.PopAsync();
        }
    }
}