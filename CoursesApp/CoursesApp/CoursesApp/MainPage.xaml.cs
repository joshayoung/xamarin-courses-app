using CoursesApp.ViewModels;
using Xamarin.Forms;

namespace CoursesApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(CourseCollectionViewModel courseCollectionViewModel)
        {
            InitializeComponent();
            BindingContext = courseCollectionViewModel;
        }
    }
}