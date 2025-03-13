using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using uniManagementApp.Views;
namespace uniManagementApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

    [ObservableProperty]
    private UserControl currentView;

    private LoginView _loginView = new LoginView{DataContext = new LoginViewModel()};
    private TeacherView _teacherView = new TeacherView{DataContext = new TeacherViewModel()};
    private StudentView _studentView = new StudentView{DataContext = new StudentViewModel()};

    public MainWindowViewModel()
    {
        CurrentView = _loginView;
    }

    [RelayCommand]
    public void NavigateToTeacherView()
    {
        CurrentView = _teacherView;
    }

    public void NavigateToStudentView()
    {
        CurrentView = _studentView;
    }
}
