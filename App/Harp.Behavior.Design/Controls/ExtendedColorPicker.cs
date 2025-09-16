using System;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace Harp.Behavior.Design.Controls;

// NOTE: This is currently needed because there's an issue in the ColorPicker control where
// the SelectedIndex is not respected on load.
public class ExtendedColorPicker : ColorPicker
{
    protected override Type StyleKeyOverride => typeof(ColorPicker);

    private int _selectedTabIndex;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        // There might be a property set in xaml that get's overridden here
        _selectedTabIndex = SelectedIndex;
        base.OnApplyTemplate(e);
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        var type = typeof(ColorView);
        if (type.GetField("_tabControl", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(this) is not TabControl tabControl)
            return;
        tabControl.SelectedIndex = _selectedTabIndex;
    }
}
