using System.Collections.Generic;
using System;

namespace uniManagementApp.Models
{
    public class Subject
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TeacherId { get; set; }
        public List<int>? StudentsEnrolled { get; set; }
        public Teacher? Teacher { get; set; } // Add a reference to the Teacher object

        private static Random random = new Random();
        public string Color { get; set; } = $"#{random.Next(0x1000000):X6}"; // Random color on creation

        public Subject(int id, string name, string description, int teacherId, List<int>? studentsEnrolled = null)
        {
            Id = id;
            Name = name;
            Description = description;
            TeacherId = teacherId;
            StudentsEnrolled = studentsEnrolled;
        }   

        public override string ToString()
        {
                return $"{Name}";
        }
    }
}