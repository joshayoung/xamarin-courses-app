using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages.modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddStudentModal : ContentPage
    {
        private readonly StudentViewModel studentViewModel;

        public AddStudentModal(StudentViewModel studentViewModel)
        {
            InitializeComponent();
            BindingContext = this.studentViewModel = studentViewModel;
        }

        private void SaveStudent(object sender, EventArgs e)
        {
                studentViewModel.AddStudent();
                Navigation.PopModalAsync();
        }

        private void CloseModal(object sender, EventArgs e) => Navigation.PopModalAsync();
    }
}