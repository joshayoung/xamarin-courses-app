using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages.modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCourseModal : ContentPage
    {
        public EditCourseModal(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            BindingContext = courseViewModel;
        }

        private void SaveCourseEdits(object sender, EventArgs e)
        {
            try
            {
                // TODO: Save to API Here
                Navigation.PopModalAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void CloseModal(object sender, EventArgs e) => Navigation.PopModalAsync();
    }
}