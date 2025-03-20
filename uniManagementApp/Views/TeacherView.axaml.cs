using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using uniManagementApp.Models;
using uniManagementApp.ViewModels;

namespace uniManagementApp.Views;

public partial class TeacherView : UserControl
{
    private Popup popup;

    public TeacherView()
    {
        InitializeComponent();
        popup = this.FindControl<Popup>("Popup");

        var viewModel = DataContext as TeacherViewModel;
        if (viewModel != null)
        {
            viewModel.SetPopup(popup);
        }
    }
}