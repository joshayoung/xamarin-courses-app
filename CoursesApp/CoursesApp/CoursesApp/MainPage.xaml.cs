using System.Collections.Generic;
using CoursesApp.Models;
using CoursesApp.ViewModels;
using Xamarin.Forms;

namespace CoursesApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(CourseCollectionViewModel courses)
        {
            InitializeComponent();
        }
    }
}