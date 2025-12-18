using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFTest.Presentation.Converters;

public class WidthStarConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            return new GridLength(width, GridUnitType.Star);
        }
        return new GridLength(1, GridUnitType.Star);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
