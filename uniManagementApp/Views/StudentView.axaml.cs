using Avalonia.Controls;
using uniManagementApp.ViewModels;

namespace uniManagementApp.Views;

public partial class StudentView : UserControl
{
    public StudentView()
    {
        InitializeComponent();
        DataContext = new StudentViewModel();
    }
}