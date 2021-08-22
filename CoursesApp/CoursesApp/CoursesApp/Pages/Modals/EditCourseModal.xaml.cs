using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages.modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCourseModal : ContentPage
    {
        private readonly string id;
        private readonly CourseViewModel courseViewModel;
        
        public EditCourseModal(string id, CourseViewModel courseViewModel)
        {
            InitializeComponent();
            this.id = id;
            BindingContext = this.courseViewModel = courseViewModel;
        }

        private void SaveCourseEdits(object sender, EventArgs e)
        {
            try
            {
                courseViewModel.SaveCourse(id);
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