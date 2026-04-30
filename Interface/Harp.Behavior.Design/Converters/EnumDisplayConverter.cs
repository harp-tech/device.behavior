using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Avalonia.Data.Converters;

namespace Harp.Behavior.Design.Converters;

public class EnumDisplayConverter : IValueConverter
{
    static readonly ConcurrentDictionary<Enum, string> _cache = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Enum e)
            return value;
        return _cache.GetOrAdd(e, static key =>
        {
            var field = key.GetType().GetField(key.ToString());
            return field?.GetCustomAttribute<DescriptionAttribute>()?.Description
                   ?? key.ToString();
        });
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}
