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
            membersView.ButtonText = "Add Student";
            membersView.Students = courseViewModel.Students;
            Console.WriteLine("tes");
        }

        private async Task AddStudent()
        {
            await Navigation.PushAsync(new AddStudentPage());
        }

        private void ViewDetails(object sender, EventArgs e)
        {
            if (!(((VisualElement)sender).BindingContext is TeacherViewModel teacherViewModel)) return;
            Navigation.PushAsync(new TeacherPage(teacherViewModel));
        }
    }
}