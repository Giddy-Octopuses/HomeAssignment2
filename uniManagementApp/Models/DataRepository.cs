using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace uniManagementApp.Models
{
    public class DataRepository
    {
        public ObservableCollection<Subject> Subjects { get; set; } = new ObservableCollection<Subject>();
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
        public ObservableCollection<Teacher> Teachers { get; set; } = new ObservableCollection<Teacher>();

        public DataRepository()
        {
            var json = File.ReadAllText("./data.json");
            var jsonData = JsonSerializer.Deserialize<JsonDataStructure>(json);
            
            if (jsonData != null)
            {
                Subjects = new ObservableCollection<Subject>(jsonData.Subjects.Values);
                Students = new ObservableCollection<Student>(jsonData.Students.Values);
                Teachers = new ObservableCollection<Teacher>(jsonData.Teachers.Values);
            }
        }
    }

    public class JsonDataStructure
    {
        public Dictionary<string, Subject> Subjects { get; set; } = new();
        public Dictionary<string, Student> Students { get; set; } = new();
        public Dictionary<string, Teacher> Teachers { get; set; } = new();
    }

}
