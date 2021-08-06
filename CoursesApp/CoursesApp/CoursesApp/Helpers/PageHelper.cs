using Xamarin.Forms;

namespace CoursesApp.Helpers
{
    public static class PageHelper
    {
        public static Label WelcomeText()
        {
            var label = new Label { FontSize = 24 };
            var formattedString = new FormattedString();
            var spanInitial = new Span { Text = "Welcome to the" };
            var spanPenultimate = new Span
            {
                Text = " Courses",
                TextColor = Color.Teal,
                FontAttributes = FontAttributes.Bold,
                FontSize = 24
            };
            var spanFinal = new Span { Text = " App. Select a course below to see the details for the class." };
            formattedString.Spans.Add(spanInitial);
            formattedString.Spans.Add(spanPenultimate);
            formattedString.Spans.Add(spanFinal);
            label.FormattedText = formattedString;
            return label;
        }
    }
}