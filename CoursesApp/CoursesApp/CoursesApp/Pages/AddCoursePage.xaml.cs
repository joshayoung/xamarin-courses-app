using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCoursePage : ContentPage
    {
        private readonly CourseViewModel courseViewModel;

        public AddCoursePage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            BindingContext = this.courseViewModel = courseViewModel;
        }

        private void SaveCourse(object sender, EventArgs e)
        {
            try
            {
                courseViewModel.AddCourse();
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