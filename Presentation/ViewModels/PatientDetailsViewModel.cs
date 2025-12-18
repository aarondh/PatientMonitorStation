namespace WPFTest.Presentation.ViewModels;

public class PatientDetailsViewModel : ViewModelBase
{
    private PatientMonitorViewModel? _patient;
    private bool _showHeartRate = true;
    private bool _showBloodPressure = true;
    private bool _showRespiratoryRate = true;
    private bool _showSpO2 = true;
    private bool _showPulseRate = true;

    public PatientMonitorViewModel? Patient
    {
        get => _patient;
        set => SetProperty(ref _patient, value);
    }

    public bool ShowHeartRate
    {
        get => _showHeartRate;
        set => SetProperty(ref _showHeartRate, value);
    }

    public bool ShowBloodPressure
    {
        get => _showBloodPressure;
        set => SetProperty(ref _showBloodPressure, value);
    }

    public bool ShowRespiratoryRate
    {
        get => _showRespiratoryRate;
        set => SetProperty(ref _showRespiratoryRate, value);
    }

    public bool ShowSpO2
    {
        get => _showSpO2;
        set => SetProperty(ref _showSpO2, value);
    }

    public bool ShowPulseRate
    {
        get => _showPulseRate;
        set => SetProperty(ref _showPulseRate, value);
    }
}
