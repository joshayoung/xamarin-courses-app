using System.Collections.Generic;
using CoursesApp.Models;
using CoursesApp.Models.Builders;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace CoursesApp
{
    public partial class App : Application
    {
        public List<Course> Courses = CourseBuilder.Build();
        
        public App()
        {
            InitializeComponent();

            var vm = new CourseCollectionViewModel(Courses);
            MainPage = new MainPage(vm);
        }

        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}