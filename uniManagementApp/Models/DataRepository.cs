using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace uniManagementApp.Models
{
    public class DataRepository
    {
        private const string DataFilePath = "../net9.0/Assets/data.json";

        public ObservableCollection<Subject> Subjects { get; private set; } = new();
        public ObservableCollection<Student> Students { get; private set; } = new();
        public ObservableCollection<Teacher> Teachers { get; private set; } = new();
        public JsonDataStructure JsonData { get; private set; } = new();

        public DataRepository()
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (!File.Exists(DataFilePath))
                {
                    Console.WriteLine($"Error: Data file not found at {DataFilePath}");
                    return;
                }

                var json = File.ReadAllText(DataFilePath);
                JsonData = JsonSerializer.Deserialize<JsonDataStructure>(json) ?? new JsonDataStructure();

                // Ensure collections are properly initialized
                Subjects = new ObservableCollection<Subject>(JsonData.Subjects != null ? JsonData.Subjects.Values : new List<Subject>());
                Students = new ObservableCollection<Student>(JsonData.Students != null ? JsonData.Students.Values : new List<Student>());
                Teachers = new ObservableCollection<Teacher>(JsonData.Teachers != null ? JsonData.Teachers.Values : new List<Teacher>());
                var subjectlist = JsonSerializer.Deserialize<List<Subject>>(json);
                Subjects = new ObservableCollection<Subject>(subjectlist ?? new List<Subject>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }
    }

    public class JsonDataStructure
    {
        public Dictionary<string, Subject>? Subjects { get; set; }
        public Dictionary<string, Student>? Students { get; set; }
        public Dictionary<string, Teacher>? Teachers { get; set; }
    }

}
