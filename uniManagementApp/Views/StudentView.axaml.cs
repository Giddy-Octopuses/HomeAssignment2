using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using uniManagementApp.ViewModels;

namespace uniManagementApp.Views
{
    public partial class StudentView : UserControl
    {
        public StudentView()
        {
            InitializeComponent();
            DataContext = new StudentViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}