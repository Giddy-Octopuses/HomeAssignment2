using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using uniManagementApp.ViewModels;

namespace uniManagementApp.Views;

public partial class TeacherView : UserControl
{
    private Popup? popup;

    public TeacherView()
    {
        InitializeComponent();
        popup = this.FindControl<Popup>("Popup");

        var viewModel = DataContext as TeacherViewModel;
        if (viewModel != null && popup != null)
        {
            // No need for SetPopup if it's not used anymore
            // viewModel.SetPopup(popup);
        }
    }
}
