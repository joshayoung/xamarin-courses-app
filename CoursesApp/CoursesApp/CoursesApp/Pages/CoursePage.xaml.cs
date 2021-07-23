using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        private CourseViewModel courseViewModel;
        
        public CoursePage(CourseViewModel courseViewModel)
        {
            InitializeComponent();

            BindingContext = this.courseViewModel = courseViewModel;
        }

        private void ViewStudents(object sender, EventArgs e) =>
            Navigation.PushAsync(new ListStudentPage(courseViewModel));

        private void ViewTeacherDetails(object sender, EventArgs e)
        {
            if (!(((VisualElement) sender).BindingContext is TeacherViewModel teacherViewModel)) return;
            Navigation.PushAsync(new TeacherPage(teacherViewModel));
        }

    }
}