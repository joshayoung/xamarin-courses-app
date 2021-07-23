using System;
using CoursesApp.Models;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTeacherPage : ContentPage
    {
        private readonly CourseViewModel courseViewModel;
        public TeacherViewModel TeacherViewModel = new TeacherViewModel(new Teacher("", 0, 0));
        
        public AddTeacherPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            this.courseViewModel = courseViewModel;

            BindingContext = TeacherViewModel;
        }

        private void SaveTeacher(object sender, EventArgs e)
        {
            var teacherViewModel = (TeacherViewModel)((BindableObject) sender).BindingContext;
            courseViewModel.Teachers.Add(teacherViewModel);
            Navigation.PopAsync();
        }
    }
}