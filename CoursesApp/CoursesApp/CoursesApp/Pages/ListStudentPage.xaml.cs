using System;
using System.Threading.Tasks;
using CoursesApp.Models;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListStudentPage : ContentPage
    {
        public StudentViewModel StudentViewModel = new StudentViewModel(new Student("", 0, ""));
        public CourseViewModel CourseViewModel { get; set; }
        public ListStudentPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            CourseViewModel = courseViewModel;
            BindingContext = courseViewModel;
        }

        private void ViewDetails(object sender, EventArgs e)
        {
            if (!(((VisualElement)sender).BindingContext is StudentViewModel studentViewModel)) return;
            Navigation.PushAsync(new StudentPage(studentViewModel));
        }

        private async void AddStudent(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new AddStudentPage(CourseViewModel));
        }

    }
}