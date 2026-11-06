using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Harp.Behavior.Design.ViewModels;
using Harp.Behavior.Design.Views;

namespace Harp.Behavior.Design;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var viewModel = new BehaviorViewModel();
            desktop.MainWindow = new MainWindow
            {
                DataContext = viewModel
            };

            var portName = GetPortName(desktop.Args);
            if (portName is not null)
                desktop.MainWindow.Opened += (_, _) => viewModel.ConnectToPort(portName);
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new BehaviorView
            {
                DataContext = new BehaviorViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    private void NativeMenuItem_OnClick(object sender, EventArgs e)
    {
        if (Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime owner &&
            owner.MainWindow is Window ownerWindow)
        {
            var about = new About() { DataContext = new AboutViewModel() };
            about.ShowDialog(ownerWindow);
        }
    }

    private static string? GetPortName(string[]? args)
    {
        if (args is null)
            return null;

        var index = Array.IndexOf(args, "--port");
        return index >= 0 && index < args.Length - 1 ? args[index + 1] : null;
    }
}
