using System;
using CoursesApp.Models;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListStudentPage : ContentPage
    {
        private readonly CourseViewModel courseViewModel;
        public ListStudentPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            this.courseViewModel = courseViewModel;
            BindingContext = courseViewModel;
        }

        private void ViewDetails(object sender, EventArgs e)
        {
            if (!(((VisualElement)sender).BindingContext is StudentViewModel studentViewModel)) return;
            Navigation.PushAsync(new StudentPage(studentViewModel));
        }

        private async void AddStudent(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new AddStudentPage(new StudentViewModel(new Student("", 0, "")), courseViewModel.Students));
            // await Navigation.PushAsync(new AddStudentPage(courseViewModel));
        }
    }
}