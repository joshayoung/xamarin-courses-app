using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListStudentPage : ContentPage
    {
        public ListStudentPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            BindingContext = courseViewModel;
        }

        private void ViewStudentDetails(object sender, EventArgs e)
        {
            if (!(((VisualElement)sender).BindingContext is StudentViewModel studentViewModel)) return;
            Navigation.PushAsync(new StudentPage(studentViewModel));
        }

        private void AddStudent(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddStudentPage());
        }
    }
}