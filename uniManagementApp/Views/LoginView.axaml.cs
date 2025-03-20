using Avalonia.Controls;
using Avalonia.Interactivity;
using uniManagementApp.ViewModels;

namespace uniManagementApp.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel vm)
        {
            vm.Username = UsernameTextBox?.Text ?? string.Empty;
            vm.Password = PasswordTextBox?.Text ?? string.Empty;
            vm.Login();
            if (ErrorMessageTextBlock != null)
            {
                ErrorMessageTextBlock.Text = vm.ErrorMessage;
            }
        }
    }

}
