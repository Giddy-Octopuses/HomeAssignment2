using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace uniManagementApp.Models
{
    public class DataRepository
    {
        private string DataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "data.json");

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
                Subjects = new ObservableCollection<Subject>(JsonData.Subjects ?? new List<Subject>());
                Students = new ObservableCollection<Student>(JsonData.Students ?? new List<Student>());
                Teachers = new ObservableCollection<Teacher>(JsonData.Teachers ?? new List<Teacher>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }
    }

    public class JsonDataStructure
    {
        public List<Subject>? Subjects { get; set; }
        public List<Student>? Students { get; set; }
        public List<Teacher>? Teachers { get; set; }
    }

}
