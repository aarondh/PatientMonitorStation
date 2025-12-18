using System.Windows;
using WPFTest.Presentation.ViewModels;

namespace WPFTest.Presentation.Views;

public partial class PatientDetailsWindow : Window
{
    public PatientDetailsWindow()
    {
        InitializeComponent();
    }

    public PatientDetailsWindow(PatientMonitorViewModel patient) : this()
    {
        var viewModel = new PatientDetailsViewModel
        {
            PatientMonitor = patient
        };
        DataContext = viewModel;
    }
}
