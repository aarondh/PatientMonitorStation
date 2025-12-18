namespace WPFTest.Presentation.ViewModels;

public class PatientDetailsViewModel : ViewModelBase
{
    private PatientMonitorViewModel? _patient;
    private readonly Dictionary<VitalSignType, VitalSignSetting> _vitalSignSettings;

    public PatientDetailsViewModel()
    {
        _vitalSignSettings = new Dictionary<VitalSignType, VitalSignSetting>
        {
            [VitalSignType.HeartRate] = new()
            {
                State = VitalSignState.Both,
                Alarm = new AlarmSetting { NormalRange = new Range(60, 100), AlarmColor = "#E74C3C" }
            },
            [VitalSignType.LeadI] = new()
            {
                State = VitalSignState.Both,
                Alarm = new AlarmSetting { NormalRange = new Range(60, 100), AlarmColor = "#E74C3C" }
            },
            [VitalSignType.LeadII] = new()
            {
                State = VitalSignState.Both,
                Alarm = new AlarmSetting { NormalRange = new Range(60, 100), AlarmColor = "#E74C3C" }
            },
            [VitalSignType.LeadIII] = new()
            {
                State = VitalSignState.Both,
                Alarm = new AlarmSetting { NormalRange = new Range(60, 100), AlarmColor = "#E74C3C" }
            },
            [VitalSignType.LeadAVR] = new()
            {
                State = VitalSignState.Both,
                Alarm = new AlarmSetting { NormalRange = new Range(60, 100), AlarmColor = "#E74C3C" }
            },
            [VitalSignType.LeadAVL] = new()
            {
                State = VitalSignState.Both,
                Alarm = new AlarmSetting { NormalRange = new Range(60, 100), AlarmColor = "#E74C3C" }
            },
            [VitalSignType.BloodPressure] = new()
            {
                State = VitalSignState.Both,
                Alarm = new AlarmSetting { NormalRange = new Range(90, 140), AlarmColor = "#E74C3C" }
            },
            [VitalSignType.RespiratoryRate] = new()
            {
                State = VitalSignState.Both,
                Alarm = new AlarmSetting { NormalRange = new Range(12, 20), AlarmColor = "#E74C3C" }
            },
            [VitalSignType.SpO2] = new()
            {
                State = VitalSignState.Both,
                Alarm = new AlarmSetting { NormalRange = new Range(95, 100), AlarmColor = "#E74C3C" }
            },
            [VitalSignType.PulseRate] = new()
            {
                State = VitalSignState.Both,
                Alarm = new AlarmSetting { NormalRange = new Range(60, 100), AlarmColor = "#E74C3C" }
            }
        };
    }

    public PatientMonitorViewModel? Patient
    {
        get => _patient;
        set => SetProperty(ref _patient, value);
    }

    public Dictionary<VitalSignType, VitalSignSetting> VitalSignSettings => _vitalSignSettings;

    // Individual accessors for easier binding
    public VitalSignSetting HeartRateSetting => _vitalSignSettings[VitalSignType.HeartRate];
    public VitalSignSetting LeadISetting => _vitalSignSettings[VitalSignType.LeadI];
    public VitalSignSetting LeadIISetting => _vitalSignSettings[VitalSignType.LeadII];
    public VitalSignSetting LeadIIISetting => _vitalSignSettings[VitalSignType.LeadIII];
    public VitalSignSetting LeadAVRSetting => _vitalSignSettings[VitalSignType.LeadAVR];
    public VitalSignSetting LeadAVLSetting => _vitalSignSettings[VitalSignType.LeadAVL];
    public VitalSignSetting BloodPressureSetting => _vitalSignSettings[VitalSignType.BloodPressure];
    public VitalSignSetting RespiratoryRateSetting => _vitalSignSettings[VitalSignType.RespiratoryRate];
    public VitalSignSetting SpO2Setting => _vitalSignSettings[VitalSignType.SpO2];
    public VitalSignSetting PulseRateSetting => _vitalSignSettings[VitalSignType.PulseRate];

    // Compatibility properties for existing XAML bindings
    public bool ShowHeartRate
    {
        get => _vitalSignSettings[VitalSignType.HeartRate].State != VitalSignState.None;
        set => _vitalSignSettings[VitalSignType.HeartRate].State = value ? VitalSignState.Both : VitalSignState.None;
    }

    public bool ShowBloodPressure
    {
        get => _vitalSignSettings[VitalSignType.BloodPressure].State != VitalSignState.None;
        set => _vitalSignSettings[VitalSignType.BloodPressure].State = value ? VitalSignState.Both : VitalSignState.None;
    }

    public bool ShowRespiratoryRate
    {
        get => _vitalSignSettings[VitalSignType.RespiratoryRate].State != VitalSignState.None;
        set => _vitalSignSettings[VitalSignType.RespiratoryRate].State = value ? VitalSignState.Both : VitalSignState.None;
    }

    public bool ShowSpO2
    {
        get => _vitalSignSettings[VitalSignType.SpO2].State != VitalSignState.None;
        set => _vitalSignSettings[VitalSignType.SpO2].State = value ? VitalSignState.Both : VitalSignState.None;
    }

    public bool ShowPulseRate
    {
        get => _vitalSignSettings[VitalSignType.PulseRate].State != VitalSignState.None;
        set => _vitalSignSettings[VitalSignType.PulseRate].State = value ? VitalSignState.Both : VitalSignState.None;
    }
}
