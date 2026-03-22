using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Media;
using ReactiveUI;

namespace Harp.Behavior.Design.Adapters;

public class RgbColorItem : ReactiveObject
{
    private Color _color;
    public Color Color
    {
        get => _color;
        set => this.RaiseAndSetIfChanged(ref _color, value);
    }
}

public enum RgbKind
{
    None,
    Simple,
    Array,
    Structured,
    Complex
}

public class RgbRegisterAdapter : ReactiveObject
{
    public ObservableCollection<RgbColorItem> Colors { get; }
    public bool IsWritable { get; }
    public string RegisterKey { get; }
    public RgbKind Kind { get; }

    // For simple/structured: expose a single color property
    public Color Color
    {
        get => Colors.Count > 0 ? Colors[0].Color : Avalonia.Media.Colors.Transparent;
        set { if (Colors.Count > 0) Colors[0].Color = value; this.RaisePropertyChanged(nameof(Color)); }
    }

    private RgbRegisterAdapter _linkedAdapter;
    private int _linkedOffset;

    /// <summary>
    /// Used for design-time data or default initialization.
    /// </summary>
    public RgbRegisterAdapter()
        : this("DefaultKey", 1, true, RgbKind.Simple)
    {
    }

    public RgbRegisterAdapter(string registerKey, int colorCount, bool isWritable, RgbKind kind)
    {
        RegisterKey = registerKey;
        IsWritable = isWritable;
        Kind = kind;
        Colors = new ObservableCollection<RgbColorItem>(
            Enumerable.Range(0, colorCount).Select(_ => new RgbColorItem()));
    }

    /// <summary>
    /// Updates the adapter from a register value (byte[] or struct).
    /// </summary>
    public void UpdateFromRegister(object registerValue)
    {
        if (registerValue is byte[] arr && arr.Length >= Colors.Count * 3)
        {
            for (int i = 0; i < Colors.Count; i++)
            {
                Colors[i].Color = Color.FromRgb(arr[i * 3], arr[i * 3 + 1], arr[i * 3 + 2]);
            }
        }
        // Handle struct/complex types (e.g., with Red0, Green0, Blue0, Red1, ...)
        else if (registerValue != null)
        {
            var type = registerValue.GetType();
            for (int i = 0; i < Colors.Count; i++)
            {
                string rName = Colors.Count == 1 ? "Red" : $"Red{i}";
                string gName = Colors.Count == 1 ? "Green" : $"Green{i}";
                string bName = Colors.Count == 1 ? "Blue" : $"Blue{i}";

                // FIXME: exchanges here to compensate the error in the firmware
                var r = GetByteField(type, registerValue, rName);
                var g = GetByteField(type, registerValue, gName);
                var b = GetByteField(type, registerValue, bName);
                Colors[i].Color = Color.FromRgb(r, g, b);
            }
        }
    }

    private static byte GetByteField(Type type, object obj, string name)
    {
        var prop = type.GetProperty(name);
        if (prop != null && prop.PropertyType == typeof(byte))
            return (byte)prop.GetValue(obj);
        var field = type.GetField(name);
        if (field != null && field.FieldType == typeof(byte))
            return (byte)field.GetValue(obj);
        return 0;
    }

    /// <summary>
    /// Converts the adapter's colors to a register value (byte[]).
    /// </summary>
    public byte[] ToRegisterValue()
    {
        var arr = new byte[Colors.Count * 3];
        for (int i = 0; i < Colors.Count; i++)
        {
            arr[i * 3] = Colors[i].Color.R;
            arr[i * 3 + 1] = Colors[i].Color.G;
            arr[i * 3 + 2] = Colors[i].Color.B;
        }
        return arr;
    }

    /// <summary>
    /// Link this adapter to a parent adapter's color collection (for subrange sync).
    /// </summary>
    public void LinkToParent(RgbRegisterAdapter parent, int offset)
    {
        _linkedAdapter = parent;
        _linkedOffset = offset;

        // Sync initial values
        for (int i = 0; i < Colors.Count; i++)
        {
            Colors[i].Color = parent.Colors[offset + i].Color;
        }

        // Subscribe to changes in this adapter and propagate to parent
        for (int i = 0; i < Colors.Count; i++)
        {
            int idx = i;
            Colors[i].WhenAnyValue(x => x.Color)
                .Skip(1)
                .Subscribe(color =>
                {
                    parent.Colors[offset + idx].Color = color;
                });

            // Subscribe to changes in parent and propagate to this adapter
            parent.Colors[offset + i].WhenAnyValue(x => x.Color)
                .Skip(1)
                .Subscribe(color =>
                {
                    Colors[idx].Color = color;
                });
        }
    }
}
