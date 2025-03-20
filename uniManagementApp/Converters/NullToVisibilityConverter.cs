using Avalonia.Markup.Xaml;

using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace uniManagementApp.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public enum Visibility
        {
            Visible,
            Collapsed
        }

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
