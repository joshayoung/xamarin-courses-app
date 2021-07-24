using System;
using CoursesApp.Models;
using CoursesApp.ViewModels;
using Xamarin.Forms;

namespace CoursesApp.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(CourseCollectionViewModel courseCollectionViewModel)
        {
            InitializeComponent();
            BindingContext = courseCollectionViewModel;
            AddWelcomeText();
        }

        private void AddWelcomeText()
        {
            var label = new Label {FontSize = 24};
            var formattedString = new FormattedString();
            var spanInitial = new Span {Text = "Welcome to the"};
            var spanPenultimate = new Span
            {
                Text = " Courses",
                TextColor = Color.Teal,
                FontAttributes = FontAttributes.Bold,
                FontSize = 24
            };
            var spanFinal = new Span {Text = " App. Select a course below to see the details for the class."};
            formattedString.Spans.Add(spanInitial);
            formattedString.Spans.Add(spanPenultimate);
            formattedString.Spans.Add(spanFinal);
            label.FormattedText = formattedString;
            welcome.Children.Add(label);
        }

        private void NavigateToDetails(object sender, EventArgs e)
        {
            var frame = (Frame) sender;
            frame.BackgroundColor = Color.Coral;

            if (!(((VisualElement) sender).BindingContext is CourseViewModel courseViewModel)) return;
            Navigation.PushAsync(new CoursePage(courseViewModel));
        }
    }
}