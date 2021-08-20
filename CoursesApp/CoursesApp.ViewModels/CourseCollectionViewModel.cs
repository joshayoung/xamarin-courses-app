﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public sealed class CourseCollectionViewModel : INotifyPropertyChanged
    {
        private readonly CourseCollection courseCollection;

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        // TODO: How is this updated with no 'OnPropertyChanged()':
        public bool CoursesExist => Courses.Count > 0;

        private List<CourseViewModel> courses;

        public List<CourseViewModel> Courses
        {
            get => courses;
            set
            {
                courses = value;
                // Repopulate courses after modifying course:
                OnPropertyChanged();
                
                // Also Update This Value:
                OnPropertyChanged(nameof(CoursesExist));
            }
        }

        private List<StudentViewModel> students;

        public List<StudentViewModel> Students
        {
            get => students;
            private set
            {
                students = value;
                OnPropertyChanged();
            }
        }

        public CourseCollectionViewModel(CourseCollection courseCollection)
        {
            courses = new List<CourseViewModel>();
            students = new List<StudentViewModel>();
            this.courseCollection = courseCollection;
            courseCollection.PropertyChanged += CoursesCollectionOnPropertyChanged;
            RefreshCourses();
        }

        private void CoursesCollectionOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CourseCollection.Courses)) RefreshCourses();
            if (e.PropertyName == nameof(CourseCollection.Students)) RefreshCourses();
        }

        private void RefreshCourses()
        {
            var courseList = new List<CourseViewModel>();
            var studentList = new List<StudentViewModel>();
            courseCollection.Courses.ForEach(cs =>
            {
                studentList = cs.Students.Select(id => StudentVm(id, cs)).ToList();
                courseList.Add(CourseVm(cs, studentList));
            });
            Students = studentList;
            Courses = courseList;
        }

        private CourseViewModel CourseVm(Course cs, List<StudentViewModel> studentList)
        {
            var courseVm = new CourseViewModel(cs, courseCollection)
            {
                Students = studentList
            };
            return courseVm;
        }

        private StudentViewModel StudentVm(int id, Course cs)
        {
            return new StudentViewModel(courseCollection.GetStudent(id), cs, courseCollection);
        }

        public CourseViewModel NewCourseViewModel()
        {
            return new CourseViewModel(new Course(GetNextCourseId().ToString()), courseCollection);
        }

        private int GetNextCourseId()
        {
            if (Courses.Count == 0) return 1;

            var id = int.Parse(Courses.Max(course => course.Id));

            return ++id;
        }

        public void Refresh()
        {
            // TODO: Is setting this to true, then false necessary?
            IsRefreshing = true;
            courseCollection.RepopulateCourseList();
            IsRefreshing = false;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}