using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        private readonly CourseViewModel courseViewModel;

        public CoursePage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            BindingContext = this.courseViewModel = courseViewModel;
        }

        private void AddStudent(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushModalAsync(new AddStudentPage(courseViewModel.NewStudent()));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void EditStudent(object sender, EventArgs e)
        {
            try
            {
                var studentViewModel = (StudentViewModel)((BindableObject)sender).BindingContext;
                Navigation.PushModalAsync(new EditStudentPage(studentViewModel));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
        
        private void DeleteStudent(object sender, EventArgs e)
        {
            try
            {
                var studentViewModel = (StudentViewModel)((BindableObject)sender).BindingContext;
                studentViewModel.DeleteStudent();
                Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}