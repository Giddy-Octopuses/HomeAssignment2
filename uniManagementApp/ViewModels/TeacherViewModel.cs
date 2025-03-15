using uniManagementApp.Models;
namespace uniManagementApp.ViewModels;

public partial class TeacherViewModel : ViewModelBase
{
    public Teacher Teacher { get; }
    public TeacherViewModel(Teacher teacher)
    {
        Teacher = teacher;
    }
    
    // (?) private List<int> MySubjects = Models.Teacher.Subjects; 

    public TeacherViewModel()
    {
        MySubjects = DataRepository.LoadData.Teachers(); // I'm not sure (how to load only the subjects of the specific teacher)
    } // need to create datarepository in this class
    // iterate through datarepository.Teachers -- find the correct teacher -- find the Subjects list

    public ObservableCollection<Subject> MySubjects { get; } 

    [ObservableProperty]
    private string? newSubjectName;

    [ObservableProperty]
    private string? newSubjectDescription;

    private Subject? _selectedSubject;
    public Subject? SelectedSubject
    {
        get => _selectedSubject;
        set => SetProperty(ref _selectedSubject, value);
    }

    [RelayCommand]
    public void AddSubject()
    {
        if (string.IsNullOrWhiteSpace(NewSubjectName) || string.IsNullOrWhiteSpace(NewSubjectDescription))
        {
            return;
        }

        MySubjects.Add(new Subject { Name = NewSubjectName!, Description = NewSubjectDescription! });
        NewSubjectName = NewSubjectDescription = null;
    }

    [RelayCommand] 
    public void DeleteSubject()
    {
        if (SelectedItem != null)
        {
            MySubjects.Remove(SelectedItem);
            // MISSING: also remove from AvailableSubjects for students
            SelectedItem = null; // Clear the selection
        }
    }
}
