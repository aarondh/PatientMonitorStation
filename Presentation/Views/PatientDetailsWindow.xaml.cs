using System.Windows;
using WPFPatientMonitor.Presentation.ViewModels;

namespace WPFPatientMonitor.Presentation.Views;

public partial class PatientDetailsWindow : Window
{
    public PatientDetailsWindow()
    {
        InitializeComponent();
        DataContext = this;
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
