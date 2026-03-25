using System;
using System.Globalization;
using System.Reflection;
using Avalonia.Data.Converters;

namespace Harp.Behavior.Design.Converters;

public class PayloadFieldConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null || parameter?.ToString() is not string fieldName)
            return null;

        var valueType = value.GetType();

        // Handle struct types directly with field/property access
        if (valueType.IsValueType && !valueType.IsPrimitive)
        {
            // Try direct field access first
            var fieldInfo = valueType.GetField(fieldName);
            if (fieldInfo != null)
            {
                return fieldInfo.GetValue(value);
            }

            // Try property access second
            var propInfo = valueType.GetProperty(fieldName);
            if (propInfo != null)
            {
                return propInfo.GetValue(value);
            }
        }

        // For primitive types, use bit masking
        // This handles any register with a maskType like DigitalOutputSyncPayload 
        // that is actually a simple number but needs masking
        try
        {
            int mask = GetMaskForField(valueType, fieldName);
            if (mask != 0)
            {
                int rawValue = System.Convert.ToInt32(value);
                int shift = GetShiftForMask(mask);
                int maskedValue = (rawValue & mask) >> shift;

                // If targetType is an enum, convert to that enum type
                if (targetType.IsEnum)
                {
                    return Enum.ToObject(targetType, maskedValue);
                }
                return maskedValue;
            }
        }
        catch
        {
            // Silently continue if mask extraction fails
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null || parameter?.ToString() is not string fieldName)
            return null;

        // Handle struct types - boxed copy approach
        if (targetType.IsValueType && !targetType.IsPrimitive)
        {
            // Create a copy of the current target value if it exists
            var currentValue = Activator.CreateInstance(targetType);

            // Set the field/property on the copy
            var fieldInfo = targetType.GetField(fieldName);
            if (fieldInfo is not null && currentValue is not null)
            {
                object boxedCopy = currentValue;
                fieldInfo.SetValue(boxedCopy, value);
                return boxedCopy;
            }

            var propInfo = targetType.GetProperty(fieldName);
            if (propInfo is not null && propInfo.CanWrite && currentValue is not null)
            {
                object boxedCopy = currentValue;
                propInfo.SetValue(boxedCopy, value);
                return boxedCopy;
            }
        }

        // For primitive/enum types with bitmasks
        try
        {
            int mask = GetMaskForField(targetType, fieldName);
            if (mask != 0)
            {
                // Get the current value if available
                int currentValue = 0;

                // Extract the value from the selected enum
                int newValue = System.Convert.ToInt32(value);
                int shift = GetShiftForMask(mask);

                // Apply the new value at the correct bit position
                int result = (currentValue & ~mask) | ((newValue << shift) & mask);
                return System.Convert.ChangeType(result, targetType);
            }
        }
        catch
        {
            // Silently continue if mask extraction fails
        }

        // Default conversion
        return value;
    }

    private static int GetMaskForField(Type type, string fieldName)
    {
        // Try to find mask by reflection from payload specification
        // This assumes there's a static class or field with mask information
        try
        {
            if (type.Namespace is string typeNamespace &&
                Type.GetType($"{typeNamespace}.{type.Name}PayloadSpec") is Type payloadSpecType)
            {
                var maskField = payloadSpecType.GetField($"{fieldName}Mask", BindingFlags.Public | BindingFlags.Static);
                if (maskField?.GetValue(null) is int value)
                    return value;
            }
        }
        catch
        {
            // Ignore and use default
        }
        return 0;
    }

    private static int GetShiftForMask(int mask)
    {
        int shift = 0;
        while ((mask & 1) == 0 && shift < 32)
        {
            mask >>= 1;
            shift++;
        }
        return shift;
    }
}
