using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using uniManagementApp.Views;
using uniManagementApp.Models;
namespace uniManagementApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

    [ObservableProperty]
    private UserControl currentView;

    private LoginView _loginView = new LoginView { DataContext = new LoginViewModel() };
    private TeacherView? _teacherView;
    private StudentView? _studentView;

    public MainWindowViewModel()
    {
        CurrentView = _loginView;
        if (_loginView.DataContext is LoginViewModel loginViewModel)
        {
            loginViewModel.NavigateToStudentView = NavigateToStudentView;
            loginViewModel.NavigateToTeacherView = NavigateToTeacherView;
        }
    }

    [RelayCommand]
    public void NavigateToTeacherView(Teacher teacher)
    {
        _teacherView = new TeacherView { DataContext = new TeacherViewModel(teacher) };
        CurrentView = _teacherView;
    }

    [RelayCommand]
    public void NavigateToStudentView(Student student)
    {
        _studentView = new StudentView(student);
        CurrentView = _studentView;
    }

}
