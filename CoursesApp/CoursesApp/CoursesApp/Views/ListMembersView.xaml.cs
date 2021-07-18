using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListMembersView : ContentView
    {
        
        public Action AddButtonAction { get; set; } = () => throw new NotImplementedException();
        public Button AddButton => addButton;
        
        public View InnerContent
        {
            get => innerContent.Content; 
            set => innerContent.Content = value;
        }
        
        public ListMembersView()
        {
            InitializeComponent();
        }
        
        void AddClicked(object sender, EventArgs e) => AddButtonAction?.Invoke();
    }
}