using System;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentPage : ContentPage
    {
        private readonly StudentViewModel studentViewModel;
        public StudentPage(StudentViewModel studentViewModel)
        {
            InitializeComponent();
            BindingContext = this.studentViewModel = studentViewModel;
        }

        private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                studentViewModel.Age = (int)picker.ItemsSource[selectedIndex];
            }
        }

        private void UpdateMajor(object sender, EventArgs e)
        {
            studentViewModel.UpdateMajor();
            Navigation.PopAsync();
        }
    }
}