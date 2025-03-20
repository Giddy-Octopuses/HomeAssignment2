using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace uniManagementApp.Models;

public class Student
{
    public int Id { get; set; } // Allow setting during deserialization
    public string Name { get; set; }
    public string Username { get; set; }
    [JsonPropertyName("Password")]
    public string PasswordHash { get; set; } // Store the hash, not the plain password
    public List<int>? EnrolledSubjects { get; set; } = [];

    // Parameterless constructor for deserialization
    public Student() { }

    public Student(int id, string name, string username, string password, List<int>? enrolledSubjects = null)
    {
        Id = id;
        Name = name;
        Username = username;
        PasswordHash = PasswordHasher.HashPassword(password); // Hash on creation
        EnrolledSubjects = enrolledSubjects;
    }

    public bool VerifyPassword(string password)
    {
        return PasswordHasher.VerifyPassword(password, PasswordHash);
    }
}
