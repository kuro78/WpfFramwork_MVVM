using Microsoft.Extensions.DependencyInjection;
using MVVMFramework.ViewModels;
using System;
using System.Windows;

namespace MVVMFramework;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public App()
    {
        Services = ConfigureServices();
        this.InitializeComponent();
    }

    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public new static App Current => (App)Application.Current;

    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
    /// </summary>
    public IServiceProvider Services { get; }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    /// <returns></returns>
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddTransient(typeof(MainViewModel));

        return services.BuildServiceProvider();
    }

}
