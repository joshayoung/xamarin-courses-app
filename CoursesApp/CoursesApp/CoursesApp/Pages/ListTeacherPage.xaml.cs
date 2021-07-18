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
        public ListTeacherPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            
            // NOTE: All of the binding happens in the view:
            // BindingContext = courseViewModel;
            
            membersView.AddButtonAction = async () => await AddTeacher();
            membersView.ButtonText = "Add Teacher";
            membersView.Teachers = courseViewModel.Teachers;
            membersView.Title = courseViewModel.Title;
        }

        private void ViewTeacherDetails(object sender, EventArgs e)
        {
            if (!(((VisualElement)sender).BindingContext is TeacherViewModel teacherViewModel)) return;
            Navigation.PushAsync(new TeacherPage(teacherViewModel));
        }

        private async Task AddTeacher()
        {
            await Navigation.PushAsync(new AddTeacherPage());
        }
    }
}