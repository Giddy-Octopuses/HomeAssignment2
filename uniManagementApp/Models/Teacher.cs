using System.Collections.Generic;

namespace uniManagementApp.Models;

public class Teacher
{
    public int Id { get; private set; }
    public string Name { get; set;}
    public string Username { get; set;}
    public string PasswordHash { get; set; } // Store the hash, not the plain password
    public List<int>? Subjects { get; set; } = []; 

    public Teacher(int id, string name, string username, string password, List<int>? subjects = null)
    {
        Id = id;
        Name = name;
        Username = username;
        PasswordHash = PasswordHasher.HashPassword(password); // Hash on creation
        Subjects = subjects;
    }

    public bool VerifyPassword(string password)
    {
        return PasswordHasher.VerifyPassword(password, PasswordHash);
    }
    
}    