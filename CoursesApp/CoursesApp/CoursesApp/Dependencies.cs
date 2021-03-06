using System;
using CoursesApp.Models;
using CoursesApp.Models.Service;
using Microsoft.Extensions.DependencyInjection;

namespace CoursesApp
{
    public static class Dependencies
    {
        private static IServiceCollection? serviceCollection;
        private static IServiceProvider? ServiceProvider { get; set; }

        public static CourseCollection? CourseCollection =>
            ServiceProvider?.GetService<CourseCollection>();

        public static void Init()
        {
            serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<CourseDataService>();
            serviceCollection.AddSingleton<CourseCollection>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}