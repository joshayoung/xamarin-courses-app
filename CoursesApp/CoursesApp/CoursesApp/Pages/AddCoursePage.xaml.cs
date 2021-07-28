using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCoursePage : ContentPage
    {
        private readonly CourseViewModel courseViewModel;

        public AddCoursePage(CourseViewModel courseViewModel)
        {
            InitializeComponent();
            BindingContext = this.courseViewModel = courseViewModel;

            DataTemplate circleImageTemplate = new DataTemplate(() =>
            {
                var entry = new Entry();
                entry.SetBinding(Entry.TextProperty, "Name");
                var stack = new StackLayout();
                stack.BindingContext = courseViewModel.Students;
                stack.Children.Add(entry);
                return stack;
            });
                
            var stackLayout = new StackLayout();
            BindableLayout.SetItemsSource(stackLayout, courseViewModel.Students);
            BindableLayout.SetItemTemplate(stackLayout, circleImageTemplate);
            StudentList.Children.Add(stackLayout);

            // var entry = new Entry();
            // entry.SetBinding(Entry.TextProperty, "Name");
            // var stack = new StackLayout();
            // stack.BindingContext = courseViewModel.Students;
            // stack.Children.Add(entry);
            //
            // StudentList.Children.Add(stack);

            // var dataTemplate = new DataTemplate(() =>
            // {
            //     var image = new Image();
            //     image.SetBinding(Image.SourceProperty, "BgImage");
            //
            //     var titleLabel = new Label
            //     {
            //         FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            //         TextColor = Color.White,
            //     };
            //     titleLabel.SetBinding(Label.TextProperty, "Title");
            //
            //     var subTitleLabel = new Label
            //     {
            //         FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            //         TextColor = Color.White,
            //     };
            //     subTitleLabel.SetBinding(Label.TextProperty, "Subtitle");
            //
            //     return new StackLayout
            //     {
            //         BackgroundColor = Color.Pink,
            //         Padding = 2,
            //         HorizontalOptions = LayoutOptions.FillAndExpand,
            //         Children = {
            //             new Frame {
            //                 Content = new AbsoluteLayout {
            //                      HorizontalOptions = LayoutOptions.FillAndExpand,
            //                      VerticalOptions = LayoutOptions.FillAndExpand,
            //                      Children = {
            //                         image,
            //                         new StackLayout {
            //                              Margin = new Thickness(20),
            //                              VerticalOptions = LayoutOptions.CenterAndExpand,
            //                              HorizontalOptions = LayoutOptions.CenterAndExpand,
            //                              Children = {
            //                                 titleLabel,
            //                                 subTitleLabel
            //                              }
            //                         }
            //                      }
            //                 }
            //             }
            //         }
            //     };
            //     // var flowList = new StackLayout();
            //     // flowList.SetD
            //     // flowList.SetBinding(BindingContext, "List");
            //     // flowList.FlowColumnTemplate = dataTemplate;
            //     // flowList.BackgroundColor = Color.LightGoldenrodYellow;
            //     // flowList.FlowColumnCount = 1;
            //     // flowList.HasUnevenRows = true;
            // });

            // var label = new Label();
            // var label2 = new Label();
            // label.SetBinding(Label.TextProperty, "Name");
            // label2.SetBinding(Label.TextProperty, "Age");
            //
            // DataTemplate myDataTemplate = new DataTemplate();
            // var stackLayout = new StackLayout();
            // myDataTemplate.SetBinding(StackLayout.BindingContextProperty, new Binding("Students"));
            // myDataTemplate.SetBinding(Label.TextProperty, new Binding("Age"));
            // BindableLayout.SetItemsSource(stackLayout, courseViewModel.Students);
            // BindableLayout.SetItemTemplate(stackLayout, myDataTemplate);
            // stackLayout.Children.Add(label);
            // stackLayout.Children.Add(label2);
            // StudentList.Children.Add(stackLayout);

        }

        private void SaveCourse(object sender, EventArgs e)
        {
            courseViewModel.AddCourse();
            Navigation.PopAsync();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            // var personDataTemplate = new DataTemplate(() =>
            // {
            //     var grid = new Grid();
            //     return new ViewCell {View = grid};
            // });
            //
            // Resources = new ResourceDictionary();
            // Resources.Add("personTemplate", personDataTemplate);
            //
            // Content = new StackLayout
            // {
            //     Margin = new Thickness(20),
            //     Children =
            //     {
            //         new ListView {ItemTemplate = (DataTemplate) Resources["personTemplate"], ItemsSource = people}
            //     }
            // };

            var label = new Label();
            var label2 = new Label();
            label.SetBinding(Label.TextProperty, "Name");
            // label2.SetBinding(Label.TextProperty, "Age");
            label2.Text = "test";

            DataTemplate myDataTemplate = new DataTemplate();
            var stackLayout = new StackLayout();
            BindableLayout.SetItemsSource(stackLayout, courseViewModel.Students);
            // BindableLayout.SetItemTemplate(stackLayout, myDataTemplate);
            stackLayout.Children.Add(label);
            stackLayout.Children.Add(label2);
            StudentList.Children.Add(stackLayout);


            // create bindable from here.
            // StudentList.BindingContext = courseViewModel.Students;
            // var text = new Entry();
            // text.BackgroundColor = Color.Aqua;
            // // text.BindingContext = courseViewModel.Students;
            // text.SetBinding(Entry.TextProperty, new Binding("Name", BindingMode.TwoWay));
            //
            // StudentList.Children.Add(text);
        }
    }
}