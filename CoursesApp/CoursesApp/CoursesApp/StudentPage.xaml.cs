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
        public StudentPage(StudentViewModel studentViewModel)
        {
            InitializeComponent();

            BindingContext = studentViewModel;
        }
    }
}