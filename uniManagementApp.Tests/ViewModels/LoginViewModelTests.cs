using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Input;
using Avalonia.Headless.XUnit;
using uniManagementApp.ViewModels;
using uniManagementApp.Views;
using Xunit;

namespace uniManagementApp.Tests
{
    public class LoginViewTests
    {
        private const string ValidJson = @"{ ""Subjects"": [{ ""Id"": 1, ""Name"": ""Math"", ""Description"": ""Mathematics is the study of numbers, shapes and patterns."", ""TeacherId"": 1, ""StudentsEnrolled"": [1] }], 
                                            ""Students"": [{ ""Id"": 1, ""Name"": ""Steve"", ""Username"": ""john123"", ""Password"": ""uGZCxIdmsEtg/SoUKbdMVg==:bj5u+2ojAhprGYwAAU2YPDtsbi/TjKztos3lBnzt3Ao="", ""EnrolledSubjects"": [1] }, { ""Id"": 2, ""Name"": ""Jane"", ""Username"": ""jane123"", ""Password"": ""3et5M7RFyxTbtdAEa6RwYA==:+gxdTzOWFHW5luBwmXdw0Si82GvjtE/IPUoH64i9ReY="", ""EnrolledSubjects"": [] }], 
                                            ""Teachers"": [{ ""Id"": 1, ""Name"": ""Mr. Schmidt"", ""Username"": ""smith123"", ""Password"": ""NIW+007DsVT+njlTpIx65Q==:gL+fCfpyB0MeKA119dGUsNsAEYImRCoKcD33ILC7tYk="", ""Subjects"": [1] }] }";

        [AvaloniaFact]
        public void Login_WithValidStudentCredentials_ShouldSucceed()
        {
            // Arrange
            var window = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };
            window.Show();

            var mainWindowViewModel = window.DataContext as MainWindowViewModel;
            Assert.NotNull(mainWindowViewModel);
            Assert.IsType<LoginView>(mainWindowViewModel.CurrentView);

            var loginView = (LoginView)mainWindowViewModel.CurrentView;
            var loginViewModel = loginView.DataContext as LoginViewModel;
            Assert.NotNull(loginViewModel);

            var filePath = "test.json";
            File.WriteAllText(filePath, ValidJson);
            loginViewModel.dataRepository.LoadData(filePath);

            // Act
            var usernameTextBox = loginView.FindControl<TextBox>("UsernameTextBox");
            Assert.NotNull(usernameTextBox);
            usernameTextBox.Focus();
            window.KeyTextInput("john123");

            var passwordTextBox = loginView.FindControl<TextBox>("PasswordTextBox");
            Assert.NotNull(passwordTextBox);
            passwordTextBox.Focus();
            window.KeyTextInput("john123");

            var loginButton = loginView.FindControl<Button>("LoginButton");
            Assert.NotNull(loginButton);
            loginButton.Focus();
            window.KeyPressQwerty(PhysicalKey.Enter, RawInputModifiers.None);

            // Assert
            var errorMessageTextBlock = loginView.FindControl<TextBlock>("ErrorMessageTextBlock");
            Assert.NotNull(errorMessageTextBlock);
            Assert.Equal("Welcome, Steve!", errorMessageTextBlock.Text);
        }

        [AvaloniaFact]
        public void Login_WithValidTeacherCredentials_ShouldSucceed()
        {
            // Arrange
            var window = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };
            window.Show();

            var mainWindowViewModel = window.DataContext as MainWindowViewModel;
            Assert.NotNull(mainWindowViewModel);
            Assert.IsType<LoginView>(mainWindowViewModel.CurrentView);

            var loginView = (LoginView)mainWindowViewModel.CurrentView;
            var loginViewModel = loginView.DataContext as LoginViewModel;
            Assert.NotNull(loginViewModel);

            var filePath = "test.json";
            File.WriteAllText(filePath, ValidJson);
            loginViewModel.dataRepository.LoadData(filePath);

            var teacherRadioButton = loginView.FindControl<RadioButton>("TeacherRadioButton");
            Assert.NotNull(teacherRadioButton);
            teacherRadioButton.IsChecked = true;

            // Act
            var usernameTextBox = loginView.FindControl<TextBox>("UsernameTextBox");
            Assert.NotNull(usernameTextBox);
            usernameTextBox.Focus();
            window.KeyTextInput("smith123");

            var passwordTextBox = loginView.FindControl<TextBox>("PasswordTextBox");
            Assert.NotNull(passwordTextBox);
            passwordTextBox.Focus();
            window.KeyTextInput("smith123");

            var loginButton = loginView.FindControl<Button>("LoginButton");
            Assert.NotNull(loginButton);
            loginButton.Focus();
            window.KeyPressQwerty(PhysicalKey.Enter, RawInputModifiers.None);

            // Assert
            var errorMessageTextBlock = loginView.FindControl<TextBlock>("ErrorMessageTextBlock");
            Assert.NotNull(errorMessageTextBlock);
            Assert.Equal("Welcome, Mr. Schmidt!", errorMessageTextBlock.Text);
        }

        [AvaloniaFact]
        public void Login_WithInvalidCredentials_ShouldShowError()
        {
            // Arrange
            var window = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };
            window.Show();

            var mainWindowViewModel = window.DataContext as MainWindowViewModel;
            Assert.NotNull(mainWindowViewModel);
            Assert.IsType<LoginView>(mainWindowViewModel.CurrentView);

            var loginView = (LoginView)mainWindowViewModel.CurrentView;
            var loginViewModel = loginView.DataContext as LoginViewModel;
            Assert.NotNull(loginViewModel);

            var filePath = "test.json";
            File.WriteAllText(filePath, ValidJson);
            loginViewModel.dataRepository.LoadData(filePath);

            // Act
            var usernameTextBox = loginView.FindControl<TextBox>("UsernameTextBox");
            Assert.NotNull(usernameTextBox);
            usernameTextBox.Focus();
            window.KeyTextInput("wrong_user");

            var passwordTextBox = loginView.FindControl<TextBox>("PasswordTextBox");
            Assert.NotNull(passwordTextBox);
            passwordTextBox.Focus();
            window.KeyTextInput("wrong_pass");

            var loginButton = loginView.FindControl<Button>("LoginButton");
            Assert.NotNull(loginButton);
            loginButton.Focus();
            window.KeyPressQwerty(PhysicalKey.Enter, RawInputModifiers.None);

            // Assert
            var errorMessageTextBlock = loginView.FindControl<TextBlock>("ErrorMessageTextBlock");
            Assert.NotNull(errorMessageTextBlock);
            Assert.Equal("Invalid username or password!", errorMessageTextBlock.Text);
        }

        [AvaloniaFact]
        public void Login_WithEmptyFields_ShouldShowError()
        {
            // Arrange
            var window = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };
            window.Show();

            var mainWindowViewModel = window.DataContext as MainWindowViewModel;
            Assert.NotNull(mainWindowViewModel);
            Assert.IsType<LoginView>(mainWindowViewModel.CurrentView);

            var loginView = (LoginView)mainWindowViewModel.CurrentView;
            var loginViewModel = loginView.DataContext as LoginViewModel;
            Assert.NotNull(loginViewModel);

            var filePath = "test.json";
            File.WriteAllText(filePath, ValidJson);
            loginViewModel.dataRepository.LoadData(filePath);

            // Act
            var loginButton = loginView.FindControl<Button>("LoginButton");
            Assert.NotNull(loginButton);
            loginButton.Focus();
            window.KeyPressQwerty(PhysicalKey.Enter, RawInputModifiers.None);

            // Assert
            var errorMessageTextBlock = loginView.FindControl<TextBlock>("ErrorMessageTextBlock");
            Assert.NotNull(errorMessageTextBlock);
            Assert.Equal("Please fill in all fields.", errorMessageTextBlock.Text);
        }

        [AvaloniaFact]
        public void Login_StudentWithTeacherCredentials_ShouldShowError()
        {
            // Arrange
            var window = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };
            window.Show();

            var mainWindowViewModel = window.DataContext as MainWindowViewModel;
            Assert.NotNull(mainWindowViewModel);
            Assert.IsType<LoginView>(mainWindowViewModel.CurrentView);

            var loginView = (LoginView)mainWindowViewModel.CurrentView;
            var loginViewModel = loginView.DataContext as LoginViewModel;
            Assert.NotNull(loginViewModel);

            var filePath = "test.json";
            File.WriteAllText(filePath, ValidJson);
            loginViewModel.dataRepository.LoadData(filePath);

            // Act
            var usernameTextBox = loginView.FindControl<TextBox>("UsernameTextBox");
            Assert.NotNull(usernameTextBox);
            usernameTextBox.Focus();
            window.KeyTextInput("smith123");

            var passwordTextBox = loginView.FindControl<TextBox>("PasswordTextBox");
            Assert.NotNull(passwordTextBox);
            passwordTextBox.Focus();
            window.KeyTextInput("smith123");

            var loginButton = loginView.FindControl<Button>("LoginButton");
            Assert.NotNull(loginButton);
            loginButton.Focus();
            window.KeyPressQwerty(PhysicalKey.Enter, RawInputModifiers.None);

            // Assert
            var errorMessageTextBlock = loginView.FindControl<TextBlock>("ErrorMessageTextBlock");
            Assert.NotNull(errorMessageTextBlock);
            Assert.Equal("Invalid username or password!", errorMessageTextBlock.Text);
        }

        [AvaloniaFact]
        public void Login_TeacherWithStudentCredentials_ShouldShowError()
        {
            // Arrange
            var window = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };
            window.Show();

            var mainWindowViewModel = window.DataContext as MainWindowViewModel;
            Assert.NotNull(mainWindowViewModel);
            Assert.IsType<LoginView>(mainWindowViewModel.CurrentView);

            var loginView = (LoginView)mainWindowViewModel.CurrentView;
            var loginViewModel = loginView.DataContext as LoginViewModel;
            Assert.NotNull(loginViewModel);

            var filePath = "test.json";
            File.WriteAllText(filePath, ValidJson);
            loginViewModel.dataRepository.LoadData(filePath);

            var teacherRadioButton = loginView.FindControl<RadioButton>("TeacherRadioButton");
            Assert.NotNull(teacherRadioButton);
            teacherRadioButton.IsChecked = true;

            // Act
            var usernameTextBox = loginView.FindControl<TextBox>("UsernameTextBox");
            Assert.NotNull(usernameTextBox);
            usernameTextBox.Focus();
            window.KeyTextInput("john123");

            var passwordTextBox = loginView.FindControl<TextBox>("PasswordTextBox");
            Assert.NotNull(passwordTextBox);
            passwordTextBox.Focus();
            window.KeyTextInput("john123");

            var loginButton = loginView.FindControl<Button>("LoginButton");
            Assert.NotNull(loginButton);
            loginButton.Focus();
            window.KeyPressQwerty(PhysicalKey.Enter, RawInputModifiers.None);

            // Assert
            var errorMessageTextBlock = loginView.FindControl<TextBlock>("ErrorMessageTextBlock");
            Assert.NotNull(errorMessageTextBlock);
            Assert.Equal("Invalid username or password!", errorMessageTextBlock.Text);
        }
    }
}
