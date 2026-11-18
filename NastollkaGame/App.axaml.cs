using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using NastollkaGame.ViewModels;
using NastollkaGame.Views;

namespace NastollkaGame;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        
    }
    public App(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            
            var vm = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            var win = _serviceProvider.GetRequiredService<MainWindow>();
            win.DataContext = vm;
            desktop.MainWindow = win;
        }

        base.OnFrameworkInitializationCompleted();
    }

   
}