using CoursesApp.Pages;
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

            MainPage = new NavigationPage(new MainPage(Dependencies.CourseCollectionViewModel));
        }
        
        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}