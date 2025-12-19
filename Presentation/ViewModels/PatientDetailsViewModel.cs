using WPFPatientMonitor.Domain.Entities;

namespace WPFPatientMonitor.Presentation.ViewModels;

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
            if (PatientMonitor.MonitorSettings.TryGetValue(type, out var setting))
            {
                return setting.State;
            }
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

    public List<MonitorSettingViewModel> ActiveMonitorSettings
    {
        get
        {
            if (PatientMonitor == null)
                return new List<MonitorSettingViewModel>();

            return PatientMonitor.MonitorSettings
                .Where(kv => kv.Value.State != MonitorViewState.None)
                .Select(kv => new MonitorSettingViewModel(kv.Value))
                .ToList();
        }
    }

    public List<MonitorSettingViewModel> AllMonitorSettings
    {
        get
        {
            if (PatientMonitor == null)
                return new List<MonitorSettingViewModel>();

            return PatientMonitor.MonitorSettings
                .Select(kv => new MonitorSettingViewModel(kv.Value))
                .ToList();
        }
    }

    // MonitorSetting properties for direct binding
    public MonitorSetting? HeartRateSetting => PatientMonitor?.MonitorSettings.GetValueOrDefault(MonitorType.HeartRate);
    public MonitorSetting? LeadISetting => PatientMonitor?.MonitorSettings.GetValueOrDefault(MonitorType.LeadI);
    public MonitorSetting? LeadIISetting => PatientMonitor?.MonitorSettings.GetValueOrDefault(MonitorType.LeadII);
    public MonitorSetting? LeadIIISetting => PatientMonitor?.MonitorSettings.GetValueOrDefault(MonitorType.LeadIII);
    public MonitorSetting? LeadAVRSetting => PatientMonitor?.MonitorSettings.GetValueOrDefault(MonitorType.LeadAVR);
    public MonitorSetting? LeadAVLSetting => PatientMonitor?.MonitorSettings.GetValueOrDefault(MonitorType.LeadAVL);
    public MonitorSetting? BloodPressureSetting => PatientMonitor?.MonitorSettings.GetValueOrDefault(MonitorType.BloodPressure);
    public MonitorSetting? RespiratoryRateSetting => PatientMonitor?.MonitorSettings.GetValueOrDefault(MonitorType.RespiratoryRate);
    public MonitorSetting? SpO2Setting => PatientMonitor?.MonitorSettings.GetValueOrDefault(MonitorType.SpO2);
    public MonitorSetting? PulseRateSetting => PatientMonitor?.MonitorSettings.GetValueOrDefault(MonitorType.PulseRate);
}
