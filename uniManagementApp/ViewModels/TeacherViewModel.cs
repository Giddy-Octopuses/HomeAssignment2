using uniManagementApp.Models;
namespace uniManagementApp.ViewModels;

public partial class TeacherViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    public Teacher Teacher { get; }
    public TeacherViewModel(Teacher teacher)
    {
        Teacher = teacher;
    }
}
