using System;
using System.Collections.Generic;
using CoursesApp.Models;
using CoursesApp.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace CoursesApp
{
    public partial class App : Application
    {
        //private readonly List<Course> allCourses = GetData();
        
        public App()
        {
            InitializeComponent();

            var vm = new CourseCollectionViewModel(new CourseCollection());
            MainPage = new NavigationPage(new MainPage(vm));
        }

        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }

        private static List<Course> GetData()
        {
            try
            {
                var api = new RestClient("http://localhost:5000/api/courses");
                api.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = api.Execute(request);

                List<Course> allCourses = JsonConvert.DeserializeObject<List<Course>>(response.Content);
                return allCourses;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}