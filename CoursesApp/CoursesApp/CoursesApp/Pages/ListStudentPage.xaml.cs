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
            BindingContext = courseViewModel;
            
            membersView.AddButtonAction = async () => await AddStudent();
        }

        private void ViewStudentDetails(object sender, EventArgs e)
        {
            if (!(((VisualElement)sender).BindingContext is StudentViewModel studentViewModel)) return;
            Navigation.PushAsync(new StudentPage(studentViewModel));
        }

        private async Task AddStudent()
        {
            await Navigation.PushAsync(new AddStudentPage());
        }
    }
}