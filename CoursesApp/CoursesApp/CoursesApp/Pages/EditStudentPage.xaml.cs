using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditStudentPage : ContentPage
    {
        private readonly StudentViewModel studentViewModel;

        public EditStudentPage(StudentViewModel studentViewModel)
        {
            InitializeComponent();
            BindingContext = this.studentViewModel = studentViewModel;
        }

        private void SaveStudentEdits(object sender, EventArgs e)
        {
            // TODO: Add method for calling service here
            Navigation.PopModalAsync();
        }

        private void DeleteStudent(object sender, EventArgs e)
        {
            studentViewModel.DeleteStudent();
            Navigation.PopModalAsync();
        }

        private void CloseModal(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}