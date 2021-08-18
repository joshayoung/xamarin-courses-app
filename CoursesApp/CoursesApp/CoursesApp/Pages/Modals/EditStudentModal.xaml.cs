using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages.modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditStudentModal : ContentPage
    {
        private readonly StudentViewModel studentViewModel;

        public EditStudentModal(StudentViewModel studentViewModel)
        {
            InitializeComponent();
            BindingContext = this.studentViewModel = studentViewModel;
        }

        private void SaveStudentEdits(object sender, EventArgs e)
        {
            studentViewModel.EditStudent();
            Navigation.PopModalAsync();
        }

        private void CloseModal(object sender, EventArgs e) => Navigation.PopModalAsync();
    }
}