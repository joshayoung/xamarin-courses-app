using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
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
            courseViewModel.EditCourse();
            Navigation.PopModalAsync();
        }

        private void CloseModal(object sender, EventArgs e) => Navigation.PopModalAsync();
    }
}