using System.Collections.ObjectModel;
using WPFTest.Application.Services;
using WPFTest.Domain.Entities;

namespace WPFTest.Presentation.ViewModels;

public class PatientMonitorViewModel : ViewModelBase, IDisposable
{
    private readonly MonitoringService _monitoringService;
    private IDisposable? _heartRateSubscription;
    private IDisposable? _bloodPressureSubscription;
    private IDisposable? _respiratorySubscription;
    private IDisposable? _pulseOximetrySubscription;
    private string _patientId = string.Empty;
    private string _profileName = string.Empty;
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private DateTime _dateOfBirth;
    private string _primaryCareGiver = string.Empty;
    private string _roomId = string.Empty;
    private int _currentHeartRate;
    private int _currentSystolic;
    private int _currentDiastolic;
    private int _currentRespiratoryRate;
    private int _currentSpO2;
    private int _currentPulseRate;
    private bool _isSelected;
    private const int MaxDataPoints = 500;

    public string PatientId
    {
        get => _patientId;
        set => SetProperty(ref _patientId, value);
    }

    public string ProfileName
    {
        get => _profileName;
        set
        {
            if (SetProperty(ref _profileName, value))
            {
                OnPropertyChanged(nameof(FullName));
            }
        }
    }
    public string FirstName
    {
        get => _firstName;
        set
        {
            if (SetProperty(ref _firstName, value))
            {
                OnPropertyChanged(nameof(FullName));
            }
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (SetProperty(ref _lastName, value))
            {
                OnPropertyChanged(nameof(FullName));
            }
        }
    }

    public string FullName => $"{FirstName} {LastName}";

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            if (SetProperty(ref _dateOfBirth, value))
            {
                OnPropertyChanged(nameof(Age));
            }
        }
    }

    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    private Dictionary<MonitorType, MonitorSetting> _monitorSettings = new()
    {
        { MonitorType.HeartRate, new MonitorSetting() },
        { MonitorType.BloodPressure, new MonitorSetting() },
        { MonitorType.RespiratoryRate, new MonitorSetting() },
        { MonitorType.SpO2, new MonitorSetting() },
        { MonitorType.PulseRate, new MonitorSetting() }
    };

    public Dictionary<MonitorType, MonitorSetting> MonitorSettings
    {

        get => _monitorSettings;
        set => SetProperty(ref _monitorSettings, value);
    }
    public string PrimaryCareGiver
    {
        get => _primaryCareGiver;
        set => SetProperty(ref _primaryCareGiver, value);
    }

    public string RoomName
    {
        get => _roomId;
        set => SetProperty(ref _roomId, value);
    }

    public int CurrentHeartRate
    {
        get => _currentHeartRate;
        set => SetProperty(ref _currentHeartRate, value);
    }

    public int CurrentSystolic
    {
        get => _currentSystolic;
        set => SetProperty(ref _currentSystolic, value);
    }

    public int CurrentDiastolic
    {
        get => _currentDiastolic;
        set => SetProperty(ref _currentDiastolic, value);
    }

    public int CurrentRespiratoryRate
    {
        get => _currentRespiratoryRate;
        set => SetProperty(ref _currentRespiratoryRate, value);
    }

    public int CurrentSpO2
    {
        get => _currentSpO2;
        set => SetProperty(ref _currentSpO2, value);
    }

    public int CurrentPulseRate
    {
        get => _currentPulseRate;
        set => SetProperty(ref _currentPulseRate, value);
    }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public ObservableCollection<int> HeartRateData { get; } = new();
    public ObservableCollection<int> LeadIData { get; } = new();
    public ObservableCollection<int> LeadIIData { get; } = new();
    public ObservableCollection<int> LeadIIIData { get; } = new();
    public ObservableCollection<int> LeadAVRData { get; } = new();
    public ObservableCollection<int> LeadAVLData { get; } = new();
    public ObservableCollection<int> RespiratoryData { get; } = new();

    public PatientMonitorViewModel(MonitoringService monitoringService)
    {
        _monitoringService = monitoringService;
    }

    public void Initialize(EpisodeOfCare episodeOfCare)
    {
        PatientId = episodeOfCare.PatientId;
        FirstName = episodeOfCare.Patient.FirstName;
        LastName = episodeOfCare.Patient.LastName;
        DateOfBirth = episodeOfCare.Patient.DateOfBirth;
        PrimaryCareGiver = episodeOfCare.Patient.PrimaryCareGiver;
        RoomName = episodeOfCare.Room.Name;
        MonitorSettings = episodeOfCare.MonitorProfile.MonitorSettings;
        StartMonitoring();
    }

    public void StartMonitoring()
    {
        if (_heartRateSubscription != null) return; // Already monitoring

        _heartRateSubscription = _monitoringService
            .MonitorHeartRate(PatientId)
            .Subscribe(reading =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    CurrentHeartRate = reading.Value;
                    HeartRateData.Add(reading.Value);
                    LeadIData.Add(reading.LeadI);
                    LeadIIData.Add(reading.LeadII);
                    LeadIIIData.Add(reading.LeadIII);
                    LeadAVRData.Add(reading.LeadAVR);
                    LeadAVLData.Add(reading.LeadAVL);

                    if (HeartRateData.Count > MaxDataPoints)
                    {
                        HeartRateData.RemoveAt(0);
                        LeadIData.RemoveAt(0);
                        LeadIIData.RemoveAt(0);
                        LeadIIIData.RemoveAt(0);
                        LeadAVRData.RemoveAt(0);
                        LeadAVLData.RemoveAt(0);
                    }
                });
            });

        _bloodPressureSubscription = _monitoringService
            .MonitorBloodPressure(PatientId)
            .Subscribe(reading =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    CurrentSystolic = reading.Systolic;
                    CurrentDiastolic = reading.Diastolic;
                });
            });

        _respiratorySubscription = _monitoringService
            .MonitorRespiratory(PatientId)
            .Subscribe(reading =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    CurrentRespiratoryRate = reading.Rate;
                    RespiratoryData.Add(reading.Rate);

                    if (RespiratoryData.Count > MaxDataPoints)
                    {
                        RespiratoryData.RemoveAt(0);
                    }
                });
            });

        _pulseOximetrySubscription = _monitoringService
            .MonitorPulseOximetry(PatientId)
            .Subscribe(reading =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    CurrentSpO2 = reading.SpO2;
                    CurrentPulseRate = reading.PulseRate;
                });
            });
    }

    public void Dispose()
    {
        _heartRateSubscription?.Dispose();
        _bloodPressureSubscription?.Dispose();
        _respiratorySubscription?.Dispose();
        _pulseOximetrySubscription?.Dispose();
        _monitoringService.StopMonitoringPatient(PatientId);
    }
}
