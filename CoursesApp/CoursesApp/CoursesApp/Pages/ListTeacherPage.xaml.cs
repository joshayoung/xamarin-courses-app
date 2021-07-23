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
            
            // NOTE: All of the binding happens in the view:
            // BindingContext = courseViewModel;
            
            membersView.AddButtonAction = async () => await AddTeacher();
            membersView.ButtonText = "Add Teacher";
            membersView.Teachers = courseViewModel.Teachers;
            membersView.Title = courseViewModel.Title;
        }

        private async Task AddTeacher()
        {
            await Navigation.PushAsync(new AddTeacherPage(CourseViewModel));
        }

        private void ViewTeacherDetails(object sender, EventArgs e)
        {
            if (!(((VisualElement)sender).BindingContext is TeacherViewModel teacherViewModel)) return;
            Navigation.PushAsync(new TeacherPage(teacherViewModel));
        }
    }
}