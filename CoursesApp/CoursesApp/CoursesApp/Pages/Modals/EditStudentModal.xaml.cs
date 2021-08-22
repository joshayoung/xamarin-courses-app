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
        private readonly int id;
        public EditStudentModal(int id, StudentViewModel studentViewModel)
        {
            InitializeComponent();
            this.id = id;
            BindingContext = this.studentViewModel = studentViewModel;
        }

        private void SaveStudentEdits(object sender, EventArgs e)
        {
            studentViewModel.SaveStudent(id, studentViewModel);
            Navigation.PopModalAsync();
        }

        private void CloseModal(object sender, EventArgs e) => Navigation.PopModalAsync();
    }
}