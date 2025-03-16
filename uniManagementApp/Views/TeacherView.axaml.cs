using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using uniManagementApp.Models;
using uniManagementApp.ViewModels;

namespace uniManagementApp.Views;

public partial class TeacherView : UserControl
{
    public TeacherView()
    {
        InitializeComponent();

        //DataContext = new TeacherViewModel(new Teacher(1, "Mr. Smith", "smith123", "smith123", new List<int> { 1 }));
    } 

    /*
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    } */
}