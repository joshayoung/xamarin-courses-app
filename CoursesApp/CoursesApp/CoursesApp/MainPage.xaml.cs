using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;

namespace CoursesApp
{
    public partial class MainPage : ContentPage
    {
        private readonly CourseCollectionViewModel courseCollectionViewModel;
        public MainPage(CourseCollectionViewModel courseCollectionViewModel)
        {
            InitializeComponent();
            BindingContext = this.courseCollectionViewModel = courseCollectionViewModel;
        }

        private void NavigateToDetails(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            frame.BackgroundColor = Color.Coral;
            
            if (!(((VisualElement)sender).BindingContext is CourseViewModel courseViewModel)) return;
            Navigation.PushAsync(new StatsPage(courseViewModel));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            courseCollectionViewModel.ReloadTheClasses();
        }
    }
}