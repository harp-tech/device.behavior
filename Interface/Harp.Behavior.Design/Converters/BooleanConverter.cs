using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Harp.Behavior.Design.Converters;




public class BooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? "Output" : "Input";
        }
        return "Input"; // Default fallback
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string stringValue)
        {
            return stringValue == "Output";
        }
        return false; // Default fallback
    }
}
