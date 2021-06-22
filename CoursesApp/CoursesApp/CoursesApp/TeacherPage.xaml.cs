using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp
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