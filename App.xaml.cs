using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WPFPatientMonitor.Domain.Ports;
using WPFPatientMonitor.Infrastructure.Adapters.EcgParameters;
using WPFPatientMonitor.Infrastructure.Adapters.RespiratoryParameter;
using WPFPatientMonitor.Infrastructure.Adapters.PulseOximetryParameter;
using WPFPatientMonitor.Infrastructure.Persistence;
using WPFPatientMonitor.Presentation.ViewModels;
using WPFPatientMonitor.Infrastructure.Adapters.BloodPressureParameter;

namespace WPFPatientMonitor;

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
        services.AddSingleton<IEcgParameters, FakeEcgParametersAdapter>();
        services.AddSingleton<IBloodPressureParameter, FakeBloodPressureParameterAdapter>();
        services.AddSingleton<IRespiratoryParameter, FakeRespiratoryParameterAdapter>();
        services.AddSingleton<IPulseOximetryParameter, FakePulseOximetryParameterAdapter>();
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
        DisposeMonitor<IEcgParameters>();
        DisposeMonitor<IBloodPressureParameter>();
        DisposeMonitor<IRespiratoryParameter>();
        DisposeMonitor<IPulseOximetryParameter>();

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

