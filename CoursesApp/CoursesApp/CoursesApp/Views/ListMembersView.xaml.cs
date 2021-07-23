using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListMembersView : ContentView
    {
        public Action AddButtonAction { get; set; }
        public Button AddButton => addButton;
        
        public string Title
        {
            get => (string) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(string)
        );

        public List<TeacherViewModel> Teachers
        {
            get => (List<TeacherViewModel>) GetValue(TeachersProperty);
            set => SetValue(TeachersProperty, value);
        }
        
        public static readonly BindableProperty TeachersProperty = BindableProperty.Create(
            nameof(Teachers),
            typeof(List<TeacherViewModel>),
            typeof(List<TeacherViewModel>)
        );
        
        public ObservableCollection<StudentViewModel> Students
        {
            get
            {
                return (ObservableCollection<StudentViewModel>) GetValue(StudentsProperty);
            }
            set => SetValue(StudentsProperty, value);
        }

        public static readonly BindableProperty StudentsProperty = BindableProperty.Create(
            nameof(Students),
            typeof(ObservableCollection<StudentViewModel>),
            typeof(ObservableCollection<StudentViewModel>)
        );

        public static readonly BindableProperty ButtonTextProperty = BindableProperty.Create(
            nameof(ButtonText),
            typeof(string),
            typeof(string),
            defaultValue: "Add",
            propertyChanged: SetButtonText
            );

        public View InnerContent
        {
            get => innerContent.Content;
            set => innerContent.Content = value;
        }

        public string ButtonText
        {
            get => (string) GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }
        
        public ListMembersView()
        {
            InitializeComponent();
            BindingContext = this;
        }
        
        void AddClicked(object sender, EventArgs e) => AddButtonAction?.Invoke();
        
        // TODO: See if the button text still works if this is removed:
        private static void SetButtonText(BindableObject bindable, object oldValue, object textValue)
        {
            var view = (ListMembersView)bindable;
            var text = (string)textValue;
            view.ButtonText = text;
        }
    }
}