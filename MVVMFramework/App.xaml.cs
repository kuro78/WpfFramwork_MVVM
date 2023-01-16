using Microsoft.Extensions.DependencyInjection;
using MVVMFramework.Controls;
using MVVMFramework.Services;
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
        var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        var services = new ServiceCollection();
        
        // ViewModel 등록
        services.AddTransient(typeof(MainViewModel));
        services.AddTransient(typeof(HomeViewModel));
        services.AddTransient(typeof(CustomerViewModel));

        // Control 등록
        services.AddTransient(typeof(AboutControl));

        // IDatabaseService 싱글톤 등록
        services.AddSingleton<IDatabaseService, SqlService>(obj => new SqlService(connectionString));

        return services.BuildServiceProvider();
    }

}
