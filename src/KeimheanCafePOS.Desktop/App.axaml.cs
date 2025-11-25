using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using KeimheanCafePOS.Desktop.ViewModels;
using KeimheanCafePOS.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using KeimheanCafePOS.Desktop.Services;

namespace KeimheanCafePOS.Desktop;

public partial class App : Application
{
    private IServiceProvider? _services;
    private AuthenticationService? _auth;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var sc = new ServiceCollection();
        ConfigureServices(sc);
        _services = sc.BuildServiceProvider();
        _auth = _services.GetRequiredService<AuthenticationService>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            DisableAvaloniaDataAnnotationValidation();
            ShowLoginWindow(desktop);
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient<AuthenticationService>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5138/");
            client.Timeout = TimeSpan.FromSeconds(30);
        });
        services.AddHttpClient<ApiService>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5138/");
            client.Timeout = TimeSpan.FromSeconds(30);
        });
        services.AddSingleton<ApiService>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<MainWindowViewModel>();
    }

    private void ShowLoginWindow(IClassicDesktopStyleApplicationLifetime desktop)
    {
        var vm = _services!.GetRequiredService<LoginViewModel>();
        var win = new LoginWindow { DataContext = vm };
        win.LoginSucceeded += () => ShowMainWindow(desktop);
        desktop.MainWindow = win;
    }

    private void ShowMainWindow(IClassicDesktopStyleApplicationLifetime desktop)
    {
        var vm = _services!.GetRequiredService<MainWindowViewModel>();
        var main = new MainWindow { DataContext = vm };
        main.Closed += (_, _) =>
        {
            _auth?.Logout();
            desktop.Shutdown();
        };
        desktop.MainWindow = main;
        main.Show();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        var pluginsToRemove = BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();
        foreach (var plugin in pluginsToRemove)
            BindingPlugins.DataValidators.Remove(plugin);
    }
}