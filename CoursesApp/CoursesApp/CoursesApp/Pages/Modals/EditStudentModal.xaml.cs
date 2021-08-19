using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages.modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditStudentModal : ContentPage
    {
        public EditStudentModal(StudentViewModel studentViewModel)
        {
            InitializeComponent();
            BindingContext = studentViewModel;
        }

        private void SaveStudentEdits(object sender, EventArgs e)
        {
            // TODO: Save to API Here
            Navigation.PopModalAsync();
        }

        private void CloseModal(object sender, EventArgs e) => Navigation.PopModalAsync();
    }
}