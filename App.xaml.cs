using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WPFTest.Domain.Ports;
using WPFTest.Infrastructure.HeartRateMonitor;
using WPFTest.Infrastructure.BloodPressureMonitor;
using WPFTest.Infrastructure.RespiratoryMonitor;
using WPFTest.Infrastructure.PulseOximetryMonitor;
using WPFTest.Infrastructure.Persistence;
using WPFTest.Presentation.ViewModels;

namespace WPFTest;

public partial class App : System.Windows.Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        // Add global exception handlers
        DispatcherUnhandledException += (s, e) =>
        {
            MessageBox.Show($"Unhandled exception: {e.Exception.Message}\n\nStack trace:\n{e.Exception.StackTrace}",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        };

        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {
            var ex = e.ExceptionObject as Exception;
            MessageBox.Show($"Fatal exception: {ex?.Message}\n\nStack trace:\n{ex?.StackTrace}",
                "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
        };

        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IHeartRateMonitor, FakeHeartRateMonitor>();
        services.AddSingleton<IBloodPressureMonitor, FakeBloodPressureMonitor>();
        services.AddSingleton<IRespiratoryMonitor, FakeRespiratoryMonitor>();
        services.AddSingleton<IPulseOximetryMonitor, FakePulseOximetryMonitor>();
        services.AddSingleton<IRoomRepository, InMemoryRoomRepository>();
        services.AddSingleton<IPatientRepository, InMemoryPatientRepository>();
        services.AddSingleton<Application.Services.MonitoringService>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Set shutdown mode to prevent premature shutdown
        ShutdownMode = ShutdownMode.OnMainWindowClose;

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        MainWindow = mainWindow;
        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        DisposeMonitor<IHeartRateMonitor>();
        DisposeMonitor<IBloodPressureMonitor>();
        DisposeMonitor<IRespiratoryMonitor>();
        DisposeMonitor<IPulseOximetryMonitor>();

        _serviceProvider.Dispose();
        base.OnExit(e);
    }

    private void DisposeMonitor<T>() where T : class
    {
        if (_serviceProvider.GetService<T>() is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}

