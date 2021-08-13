using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddStudentPage : ContentPage
    {
        private readonly StudentViewModel studentViewModel;

        public AddStudentPage(StudentViewModel studentViewModel)
        {
            InitializeComponent();
            BindingContext = this.studentViewModel = studentViewModel;
        }

        private void SaveStudent(object sender, EventArgs e)
        {
            try
            {
                // studentViewModel.AddStudent();
                Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                // TODO: Log the error
                Console.WriteLine(ex);
            }
        }

        private void CloseModal(object sender, EventArgs e) => Navigation.PopModalAsync();
    }
}