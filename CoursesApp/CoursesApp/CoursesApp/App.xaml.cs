using CoursesApp.Pages;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace CoursesApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Dependencies.Init();
            var courseCollection = Dependencies.CourseCollection;
            if (courseCollection == null) return;
            
            courseCollection.RepopulateCourseList();
            MainPage = new NavigationPage(new CoursePage(new CourseCollectionViewModel(courseCollection)));
        }

        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}