using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFTest.Application.Services;
using WPFTest.Presentation.Views;

namespace WPFTest.Presentation.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly MonitoringService _monitoringService;
    private double _miniDisplayWidth = 0.2;
    private double _detailViewWidth = 0.8;
    private PatientMonitorViewModel? _patientDoubleClicked;
    private PatientDetailsWindow? _detailsWindow;
    private PatientDetailsViewModel? _detailsViewModel;
    private const int MaxDetailedPatients = 8;
    private bool _isUpdatingSelectedStates = false;

    public ObservableCollection<PatientMonitorViewModel> AllPatients { get; } = new();
    public ObservableCollection<PatientMonitorViewModel> DetailedPatients { get; } = new();

    public double MiniDisplayWidth
    {
        get => _miniDisplayWidth;
        set
        {
            var clampedValue = Math.Clamp(value, 0.1, 0.4);
            if (SetProperty(ref _miniDisplayWidth, clampedValue))
            {
                DetailViewWidth = 1.0 - clampedValue;
            }
        }
    }

    public double DetailViewWidth
    {
        get => _detailViewWidth;
        set => SetProperty(ref _detailViewWidth, value);
    }

    public PatientMonitorViewModel? PatientDoubleClicked
    {
        get => _patientDoubleClicked;
        set
        {
            if (SetProperty(ref _patientDoubleClicked, value))
            {
                UpdateDetailsWindow();
            }
        }
    }

    public ICommand SelectPatientCommand { get; }

    public ICommand SelectPatientDetailCommand { get; }

    public MainViewModel(MonitoringService monitoringService)
    {
        _monitoringService = monitoringService;
        SelectPatientCommand = new RelayCommand<PatientMonitorViewModel>(SelectPatient);
        SelectPatientDetailCommand = new RelayCommand<PatientMonitorViewModel>(SelectPatientDetail);
        // Subscribe to collection changes to update IsSelected state

        DetailedPatients.CollectionChanged += (s, e) => UpdateSelectedStates();
    }

    public async Task InitializeAsync()
    {
        var episodes = await _monitoringService.GetAllEpisodesOfCareAsync();

        foreach (var episode in episodes)
        {
            var patientViewModel = new PatientMonitorViewModel(_monitoringService);
            // Don't initialize monitoring yet - just set properties
            patientViewModel.PatientId = episode.Patient.Id;
            patientViewModel.FirstName = episode.Patient.FirstName;
            patientViewModel.LastName = episode.Patient.LastName;
            patientViewModel.DateOfBirth = episode.Patient.DateOfBirth;
            patientViewModel.PrimaryCareGiver = episode.Patient.PrimaryCareGiver;
            patientViewModel.RoomName = episode.Room.Name;
            patientViewModel.ProfileName = episode.MonitorProfile.ProfileName;
            patientViewModel.MonitorSettings = episode.MonitorProfile.MonitorSettings;
            AllPatients.Add(patientViewModel);
        }

        // Auto-select first 8 patients for detailed view
        var initialSelection = AllPatients.Take(MaxDetailedPatients);
        foreach (var patient in initialSelection)
        {
            DetailedPatients.Add(patient);
        }

        // Ensure initial selection states are set
        UpdateSelectedStates();

        // NOW start monitoring for all patients after UI is set up
        foreach (var patientVm in AllPatients)
        {
            patientVm.StartMonitoring();
        }

        // Don't create details window on startup - only when a patient is double-clicked
        // InitializeDetailsWindow();
    }

    private void SelectPatient(PatientMonitorViewModel? patient)
    {
        if (patient == null) return;

        // Track the last clicked patient
        PatientDoubleClicked = patient;

        // If patient is already in detailed view, deselect them
        if (DetailedPatients.Contains(patient))
        {
            DetailedPatients.Remove(patient);
            return;
        }

        // If we have room, add the patient
        if (DetailedPatients.Count < MaxDetailedPatients)
        {
            DetailedPatients.Add(patient);
        }
        else
        {
            // Replace the first patient with the newly selected one
            DetailedPatients.RemoveAt(0);
            DetailedPatients.Add(patient);
        }
    }


    private void SelectPatientDetail(PatientMonitorViewModel? patient)
    {
        if (patient == null) return;

        // Track the last double clicked patient
        PatientDoubleClicked = patient;
    }

    private void InitializeDetailsWindow()
    {
        _detailsViewModel = new PatientDetailsViewModel();
        _detailsWindow = new PatientDetailsWindow
        {
            DataContext = _detailsViewModel
        };

        // Set owner after window is created, if main window is available
        if (System.Windows.Application.Current.MainWindow != null)
        {
            _detailsWindow.Owner = System.Windows.Application.Current.MainWindow;
        }

        // Recreate the window if it's closed
        _detailsWindow.Closed += (s, e) =>
        {
            _detailsWindow = null;
            _detailsViewModel = null;
        };

        // Set the patient data BEFORE showing the window
        if (PatientDoubleClicked != null)
        {
            _detailsViewModel.PatientMonitor = PatientDoubleClicked;
        }

        // Show the window
        _detailsWindow.Show();
    }

    private void UpdateDetailsWindow()
    {
        if (PatientDoubleClicked == null) return;

        // Create window on first use if it doesn't exist
        if (_detailsWindow == null)
        {
            InitializeDetailsWindow();
        }
        else
        {
            // Update the patient in the existing window
            if (_detailsViewModel != null)
            {
                _detailsViewModel.PatientMonitor = PatientDoubleClicked;
            }

            // Show the window if it's hidden or bring it to front
            if (!_detailsWindow.IsVisible)
            {
                _detailsWindow.Show();
            }
            else
            {
                _detailsWindow.Activate();
            }
        }
    }

    private void UpdateSelectedStates()
    {
        // Prevent re-entrancy
        if (_isUpdatingSelectedStates) return;

        try
        {
            _isUpdatingSelectedStates = true;

            // Update IsSelected for all patients
            foreach (var patient in AllPatients)
            {
                patient.IsSelected = DetailedPatients.Contains(patient);
            }
        }
        finally
        {
            _isUpdatingSelectedStates = false;
        }
    }
}
