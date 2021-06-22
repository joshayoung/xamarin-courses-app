using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoursesApp
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
    }
}