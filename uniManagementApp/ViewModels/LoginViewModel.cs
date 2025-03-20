using System;
using uniManagementApp.Models;

namespace uniManagementApp.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private string _username = string.Empty;
    private string _password = string.Empty;
    private string _errorMessage = string.Empty;
    private bool _isStudent = true; // Default selection
    private bool _isTeacher = false;
    public IDataRepository dataRepository = new DataRepository();

    // Navigation Actions
    public Action<Student>? NavigateToStudentView { get; set; }
    public Action<Teacher>? NavigateToTeacherView { get; set; }

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool IsStudent
    {
        get => _isStudent;
        set
        {
            SetProperty(ref _isStudent, value);
            if (value)
            {
                IsTeacher = false;
            }
        }
    }

    public bool IsTeacher
    {
        get => _isTeacher;
        set
        {
            SetProperty(ref _isTeacher, value);
            if (value)
            {
                IsStudent = false;
            }
        }
    }

    public void Login()
    {
        ErrorMessage = string.Empty;
        if (Username != "" && Password != "" && (IsStudent || IsTeacher))
        {
            ErrorMessage = "";

            if (IsStudent)
            {
                foreach (var student in dataRepository.Students)
                {
                    if (student.Username == Username && student.VerifyPassword(Password))
                    {
                        ErrorMessage = $"Welcome, {student.Name}!";
                        NavigateToStudentView?.Invoke(student);
                        return;
                    }
                }

            }
            else if (IsTeacher)
            {
                foreach (var teacher in dataRepository.Teachers)
                {
                    if (teacher.Username == Username && teacher.VerifyPassword(Password))
                    {
                        ErrorMessage = $"Welcome, {teacher.Name}!";
                        NavigateToTeacherView?.Invoke(teacher);
                        return;
                    }
                }
            }

            ErrorMessage = "Invalid username or password!";

        }
        else
        {
            ErrorMessage = "Please fill in all fields.";
        }
    }
}
