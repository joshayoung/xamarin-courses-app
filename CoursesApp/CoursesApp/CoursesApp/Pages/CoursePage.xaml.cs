using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        private readonly CourseViewModel courseViewModel;

        public CoursePage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            BindingContext = this.courseViewModel = courseViewModel;
        }

        private void AddStudent(object sender, EventArgs e)
        {
                Navigation.PushModalAsync(new AddStudentPage(courseViewModel.NewStudent()));
        }

        private void EditStudent(object sender, EventArgs e)
        {
                var studentViewModel = (StudentViewModel)((BindableObject)sender).BindingContext;
                Navigation.PushModalAsync(new EditStudentPage(studentViewModel));
        }
        
        private void DeleteStudent(object sender, EventArgs e)
        {
                var studentViewModel = (StudentViewModel)((BindableObject)sender).BindingContext;
                // studentViewModel.DeleteStudent();
                Navigation.PopModalAsync();
        }
    }
}