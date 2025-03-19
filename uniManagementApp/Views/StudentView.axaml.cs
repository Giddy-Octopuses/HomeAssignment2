using Avalonia.Controls; // For UserControl
using Avalonia.Markup.Xaml; // For AvaloniaXamlLoader
using uniManagementApp.ViewModels; // For StudentViewModel
using uniManagementApp.Models; // For Student

namespace uniManagementApp.Views
{
    public partial class StudentView : UserControl
    {
        public StudentView(Student student)
        {
            InitializeComponent();
            DataContext = new StudentViewModel(student);
        }

        public StudentView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
