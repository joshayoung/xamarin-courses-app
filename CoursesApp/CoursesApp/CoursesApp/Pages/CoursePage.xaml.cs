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

        private void AddStudent(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddStudentPage(courseViewModel.NewStudent()));
        }

        private void EditCourse(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditCoursePage(courseViewModel));
        }

        private void EditStudent(object sender, EventArgs e)
        {
            var studentViewModel = (StudentViewModel)((BindableObject) sender).BindingContext;
            Navigation.PushAsync(new EditStudentPage(studentViewModel));
        }
    }
}