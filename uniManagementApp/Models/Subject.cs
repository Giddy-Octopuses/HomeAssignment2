using System.Collections.Generic;

namespace uniManagementApp.Models;

public class Subject
{
    public int Id { get; private set; }
    public string Name { get; set;}
    public string Description { get; set;}
    public int TeacherId { get; private set; }
    public List<int>? StudentsEnrolled { get; set; }

    public Subject(int id, string name, string description, int teacherId)
    {
        Id = id;
        Name = name;
        Description = description;
        TeacherId = teacherId;
    }
}