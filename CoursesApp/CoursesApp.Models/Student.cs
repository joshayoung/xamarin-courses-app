using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CoursesApp.Models
{
    public class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private string id;
        public string Id
        {
            get => id;
            set
            {
                id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        
        private int age;
        public int Age
        {
            get => age;
            set
            {
                age = value;
                NotifyPropertyChanged(nameof(Age));
            }
        }
        
        private string major;
        public string Major
        {
            get => major;
            set
            {
                major = value;
                NotifyPropertyChanged(nameof(Major));
            }
        }

        public Student(string name, int age, string major)
        {
            Name = name;
            Age = age;
            Major = major;
        }

        public void UpdateMajor(string id)
        {
            var api = new RestClient($"http://localhost:5000/api/courses/{id}");
            api.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            JObject jObjectbody = new JObject();
            jObjectbody.Add("Title", Name);
            jObjectbody.Add("designation", Age);

            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            IRestResponse response = api.Execute(request);
        }
        
        private void NotifyPropertyChanged(string theName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(theName));
        }
    }
}