using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherPage : ContentPage
    {
        public TeacherPage(TeacherViewModel teacherViewModel)
        {
            InitializeComponent();

            BindingContext = teacherViewModel;
        }
    }
}