using System;
using CoursesApp.Pages.modals;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailsPage : ContentPage
    {
        private readonly CourseViewModel courseViewModel;

        public CourseDetailsPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            BindingContext = this.courseViewModel = courseViewModel;
            courseViewModel.UpdateAverageAge();
        }

        // protected override void OnAppearing()
        // {
        //     base.OnAppearing();
        //     courseViewModel.UpdateAverageAge();
        // }

        private void AddStudent(object sender, EventArgs e) =>
            Navigation.PushModalAsync(new AddStudentModal(courseViewModel.NewStudent()));

        private void EditStudent(object sender, EventArgs e)
        {
            var studentViewModel = (StudentViewModel)((BindableObject)sender).BindingContext;
            Navigation.PushModalAsync(new EditStudentModal(studentViewModel.Id, studentViewModel.EditStudentCopy()));
        }

        private void DeleteStudent(object sender, EventArgs e)
        {
            var studentViewModel = (StudentViewModel)((BindableObject)sender).BindingContext;
            studentViewModel.DeleteStudent();
            Navigation.PopModalAsync();
        }
    }
}