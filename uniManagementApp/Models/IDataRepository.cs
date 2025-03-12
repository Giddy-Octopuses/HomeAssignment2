using System.Collections.ObjectModel;
using uniManagementApp.Models;
public interface IDataRepository
{
    ObservableCollection<Subject> Subjects { get; }
    ObservableCollection<Student> Students { get; }
    ObservableCollection<Teacher> Teachers { get; }
}
