using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages.modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCourseModal : ContentPage
    {
        private readonly CourseViewModel courseViewModel;

        public AddCourseModal(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            BindingContext = this.courseViewModel = courseViewModel;
        }

        private void SaveCourse(object sender, EventArgs e)
        {
                courseViewModel.AddCourse();
                Navigation.PopModalAsync();
        }

        private void CloseModal(object sender, EventArgs e) => Navigation.PopModalAsync();
    }
}