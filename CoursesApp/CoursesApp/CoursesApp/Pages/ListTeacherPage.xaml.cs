using System;
using System.Threading.Tasks;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListTeacherPage : ContentPage
    {
        public CourseViewModel CourseViewModel { get; set; }
        
        public ListTeacherPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            CourseViewModel = courseViewModel;
            
            BindingContext = courseViewModel;
        }

        private void ViewTeacherDetails(object sender, EventArgs e)
        {
            if (!(((VisualElement)sender).BindingContext is TeacherViewModel teacherViewModel)) return;
            Navigation.PushAsync(new TeacherPage(teacherViewModel));
        }

        private void AddClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTeacherPage(CourseViewModel));
        }
    }
}