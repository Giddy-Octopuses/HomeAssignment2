using Avalonia.Controls;

namespace uniManagementApp.Views;

public partial class TeacherView : UserControl
{
    public TeacherView()
    {
        InitializeComponent();
        DataContext = new TeacherViewModel();
    }
}