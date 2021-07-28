using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        public CoursePage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            BindingContext = courseViewModel;
        }
    }
}