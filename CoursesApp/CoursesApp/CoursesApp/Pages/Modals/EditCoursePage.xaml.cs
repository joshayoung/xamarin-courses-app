using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages.modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCoursePage : ContentPage
    {
        private readonly CourseViewModel courseViewModel;

        public EditCoursePage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            BindingContext = this.courseViewModel = courseViewModel;
        }

        private void SaveCourseEdits(object sender, EventArgs e)
        {
            try
            {
                courseViewModel.EditCourse();
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