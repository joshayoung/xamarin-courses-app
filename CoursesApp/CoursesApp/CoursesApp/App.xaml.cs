using CoursesApp.Pages;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace CoursesApp
{
    public partial class App : Application
    {
        private readonly CourseCollectionViewModel courseCollectionViewModel;
        
        public App()
        {
            InitializeComponent();
            
            Dependencies.Init();
            courseCollectionViewModel = new CourseCollectionViewModel(Dependencies.CourseCollection);

            MainPage = new NavigationPage(new MainPage(courseCollectionViewModel));
        }

        protected override void OnStart()
        {
            courseCollectionViewModel.ReloadTheClasses();
        }
        
        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}