using System.Windows;
using WPFTest.Presentation.ViewModels;

namespace WPFTest;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;

    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // Defer initialization to after window is fully rendered
        // Use Background priority to ensure all layout is complete
        Dispatcher.BeginInvoke(async () =>
        {
            await _viewModel.InitializeAsync();
        }, System.Windows.Threading.DispatcherPriority.Background);
    }
}