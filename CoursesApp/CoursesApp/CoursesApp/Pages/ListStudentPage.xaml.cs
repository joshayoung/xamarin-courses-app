using System;
using System.Threading.Tasks;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListStudentPage : ContentPage
    {
        public ListStudentPage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            
            // NOTE: All of the binding happens in the view:
            // BindingContext = courseViewModel;
            
            membersView.AddButtonAction = async () => await AddStudent();
            membersView.ButtonText = "Add Student";
            membersView.Students = courseViewModel.Students;
            membersView.Title = courseViewModel.Title;
        }

        private async Task AddStudent()
        {
            await Navigation.PushAsync(new AddStudentPage());
        }

        private void ViewDetails(object sender, EventArgs e)
        {
            if (!(((VisualElement)sender).BindingContext is StudentViewModel studentViewModel)) return;
            Navigation.PushAsync(new StudentPage(studentViewModel));
        }
    }
}