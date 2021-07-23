using System;
using System.Collections.Generic;
using System.ComponentModel;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Student student;
        
        public List<int> Ages
        {
            get
            {
                return new List<int>()
                {
                    student.Age,
                    1,
                    2,
                    3
                };
            }
        }
        
        public string Name
        {
            get => student.Name;
            set
            {
                student.Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public int Age
        {
            get => student.Age;
            set
            {
                student.Age = value;
                NotifyPropertyChanged(nameof(Age));
            }
        }

        public string Major
        {
            get
            {
                return student.Major;
            }
            set
            {
                student.Major = value;
                NotifyPropertyChanged(nameof(Major));
            }
        }

        public StudentViewModel(Student student)
        {
            this.student = student;
            
            // Update the model:
            student.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }


        // A button would call this:
        public void UpdateMajor() => student.UpdateMajor(student.Id);

        private void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}