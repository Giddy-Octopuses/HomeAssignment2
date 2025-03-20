using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace uniManagementApp.Models
{
    public class DataRepository : IDataRepository
    {
        public string DataFilePath = Path.Combine("data.json");

        public ObservableCollection<Subject> Subjects { get; private set; } = new();
        public ObservableCollection<Student> Students { get; private set; } = new();
        public ObservableCollection<Teacher> Teachers { get; private set; } = new();
        public JsonDataStructure JsonData { get; private set; } = new();

        public DataRepository()
        {
            LoadData(DataFilePath);
        }

        public void LoadData( string filepath )
        {
            try
            {
                if (!File.Exists(filepath))
                {
                    Console.WriteLine($"Error: Data file not found at {filepath}");
                    return;
                }

                var json = File.ReadAllText(filepath);
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

        public void SaveData( string filepath )
        {
            try
            {
                JsonData.Subjects = new List<Subject>(Subjects);
                JsonData.Students = new List<Student>(Students);
                JsonData.Teachers = new List<Teacher>(Teachers);

                var json = JsonSerializer.Serialize(JsonData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filepath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        public Teacher? FindTeacher(string username, string password)
        {
            foreach (Teacher teacher in Teachers)
            {
                if (teacher.Username == username && teacher.VerifyPassword(password))
                {
                    return teacher;
                }
            }
            return null;
        }

        public Student? FindStudent(string username, string password)
        {
            foreach (Student student in Students)
            {
                if (student.Username == username && student.VerifyPassword(password))
                {
                    return student;
                }
            }
            return null;
        }

        public Subject? FindSubject(int id)
        {
            foreach (Subject subject in Subjects)
            {
                if (subject.Id == id)
                {
                    return subject;
                }
            }
            return null;
        }

        
        public Subject? LoadSubjectById(int subjectId)
        {
            foreach (var subject in Subjects)
        {
                if (string.IsNullOrEmpty(subject.Color))
            {
                    subject.Color = $"#{new Random().Next(0x1000000):X6}";
            }
        }

            return Subjects.FirstOrDefault(s => s.Id == subjectId);
        }

    }
    public class JsonDataStructure
    {
        public List<Subject>? Subjects { get; set; }
        public List<Student>? Students { get; set; }
        public List<Teacher>? Teachers { get; set; }
    }
}