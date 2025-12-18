using WPFTest.Domain.Entities;

namespace WPFTest.Presentation.ViewModels;

public class PatientDetailsViewModel : ViewModelBase
{
    private PatientMonitorViewModel? _patientMonitor;

    public PatientDetailsViewModel()
    {
    }

    public PatientMonitorViewModel? PatientMonitor
    {
        get => _patientMonitor;
        set => SetProperty(ref _patientMonitor, value);
    }

    private void SetVitalSignState(MonitorType type, MonitorViewState state)
    {
        if (PatientMonitor != null)
        {
            PatientMonitor.MonitorSettings[type].State = state;
        }
    }

    private MonitorViewState GetVitalSignState(MonitorType type)
    {
        if (PatientMonitor != null)
        {
            return PatientMonitor.MonitorSettings[type].State;
        }
        return MonitorViewState.None;
    }

    // Compatibility properties for existing XAML bindings
    public bool ShowHeartRate
    {
        get => GetVitalSignState(MonitorType.HeartRate) != MonitorViewState.None;
        set => SetVitalSignState(MonitorType.HeartRate, value ? MonitorViewState.Both : MonitorViewState.None);
    }

    public bool ShowBloodPressure
    {
        get => GetVitalSignState(MonitorType.BloodPressure) != MonitorViewState.None;
        set => SetVitalSignState(MonitorType.BloodPressure, value ? MonitorViewState.Both : MonitorViewState.None);
    }

    public bool ShowRespiratoryRate
    {
        get => GetVitalSignState(MonitorType.RespiratoryRate) != MonitorViewState.None;
        set => SetVitalSignState(MonitorType.RespiratoryRate, value ? MonitorViewState.Both : MonitorViewState.None);
    }

    public bool ShowSpO2
    {
        get => GetVitalSignState(MonitorType.SpO2) != MonitorViewState.None;
        set => SetVitalSignState(MonitorType.SpO2, value ? MonitorViewState.Both : MonitorViewState.None);
    }

    public bool ShowPulseRate
    {
        get => GetVitalSignState(MonitorType.PulseRate) != MonitorViewState.None;
        set => SetVitalSignState(MonitorType.PulseRate, value ? MonitorViewState.Both : MonitorViewState.None);
    }
}
