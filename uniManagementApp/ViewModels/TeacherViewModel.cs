using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using System.Linq;
using uniManagementApp.Models;

namespace uniManagementApp.ViewModels
{
    public partial class TeacherViewModel : ViewModelBase
    {
        public Teacher Teacher { get; }
        private readonly DataRepository _dataRepository;

        public TeacherViewModel(Teacher teacher)
        {
            Teacher = teacher;
            _dataRepository = new DataRepository();
            SubjectAll = TurnSubjectIdIntoAll(Teacher.Subjects);
        }

        private List<Subject>? _subjectAll;
        public List<Subject>? SubjectAll
        {
            get => _subjectAll;
            set => SetProperty(ref _subjectAll, value);
        }

        public List<Subject>? TurnSubjectIdIntoAll(List<int> subjectIds)
        {
            var subjectAll = new List<Subject>();
            foreach (int subjectId in subjectIds)
            {
                var subject = _dataRepository.FindSubject(subjectId);
                if (subject != null)
                {
                    subjectAll.Add(subject);
                }
            }
            return subjectAll;
        } 

        [ObservableProperty]
        private Subject? selectedSubject;
    }

    // Used to determine whether a subject was selected from the listbox.
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

/*    

[ObservableProperty]
private string? newSubjectName;

[ObservableProperty]
private string? newSubjectDescription; */

/* 
[RelayCommand]
public void AddSubject()
{
    if (string.IsNullOrWhiteSpace(NewSubjectName) || string.IsNullOrWhiteSpace(NewSubjectDescription))
    {
        return;
    }

    MySubjects.Add(new Subject { Name = NewSubjectName!, Description = NewSubjectDescription! });
    NewSubjectName = NewSubjectDescription = null;
}

[RelayCommand] 
public void DeleteSubject()
{
    if (SelectedItem != null)
    {
        MySubjects.Remove(SelectedItem);
        // MISSING: also remove from AvailableSubjects for students
        SelectedItem = null; // Clear the selection
    }
} */