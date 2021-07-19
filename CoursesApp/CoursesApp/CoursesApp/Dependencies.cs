using System;
using CoursesApp.Models;
using CoursesApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CoursesApp
{
    public static class Dependencies
    {
        private static IServiceCollection serviceCollection;
        private static IServiceProvider serviceProvider { get; set; }

        public static CourseCollectionViewModel CourseCollectionViewModel =>
            serviceProvider.GetService<CourseCollectionViewModel>();

        public static void Init()
        {
            serviceCollection = new ServiceCollection();
            var courseCollection = new CourseCollection();
            var courseCollectionViewModel = new CourseCollectionViewModel(courseCollection);
            courseCollectionViewModel.ReloadTheClasses();
            serviceCollection.AddSingleton(courseCollectionViewModel);

            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}