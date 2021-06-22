using System;
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

        private void NavigateToDetails(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            frame.BackgroundColor = Color.Coral;
            
            if (!(((VisualElement)sender).BindingContext is CourseViewModel courseViewModel)) return;
            
            Navigation.PushAsync(new CoursePage(courseViewModel));
        }
    }
}