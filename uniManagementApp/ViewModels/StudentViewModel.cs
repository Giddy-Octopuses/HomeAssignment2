using uniManagementApp.Models;
namespace uniManagementApp.ViewModels;

public partial class StudentViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    public Student Student { get; }

    public StudentViewModel(Student student)
    {
        Student = student;
    }
}
