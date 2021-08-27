﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace CoursesApp.Models
{
    public class Course : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int AverageStudentAge { get; private set; }
        
        public string? OldestStudent { get; private set; }

        public void UpdateOldestStudent(CourseCollection courseCollection)
        {
            if (Students.Count == 0)
            {
                OldestStudent = "1";
            }

            var age = 0;
            string? name = null;
            foreach (var student in Students.Select(id => courseCollection.GetStudent(id))
                .Where(student => student.Age > age))
            {
                age = student.Age;
                name = student.Name;
            }

            OldestStudent = name;
            NotifyPropertyChanged(nameof(OldestStudent));
        }

        public virtual void UpdateAverageAge(CourseCollection courseCollection)
        {
                if (Students.Count == 0) return;

                AverageStudentAge = Students
                    .Select(id => courseCollection.GetStudent(id))
                    .Select(student => student.Age)
                    .Sum() / Students.Count;
                NotifyPropertyChanged(nameof(AverageStudentAge));
        }
        
        public int Id { get; }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }

        private float length;
        public float Length
        {
            get => length;
            set
            {
                length = value;
                NotifyPropertyChanged();
            }
        }

        private List<int> students;
        public List<int> Students
        {
            get => students;
            set
            {
                students = value;
                NotifyPropertyChanged();
            }
        }

        private CourseType type;
        public CourseType Type
        {
            get => type;
            set
            {
                type = value;
                NotifyPropertyChanged();
            }
        }

        [JsonConstructor]
        public Course(
            int id,
            string title = "",
            float length = 0,
            CourseType type = CourseType.None,
            List<int>? students = null)
        {
            Id = id;
            this.title = title;
            this.length = length;
            this.type = type;
            this.students = students ?? new List<int>();
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}