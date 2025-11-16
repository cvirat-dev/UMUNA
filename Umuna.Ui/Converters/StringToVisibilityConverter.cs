using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Umuna.Ui.Converters
{
    /// <summary>
    /// Converts a string to Visibility. Returns Visible if string is not null or empty, otherwise Collapsed.
    /// </summary>
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && !string.IsNullOrWhiteSpace(str))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
