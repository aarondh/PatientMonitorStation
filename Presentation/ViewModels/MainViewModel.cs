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
        var rooms = await _monitoringService.GetAllRoomsAsync();

        foreach (var room in rooms)
        {
            var patients = await _monitoringService.GetPatientsByRoomIdAsync(room.Id);

            foreach (var patient in patients)
            {
                var patientViewModel = new PatientMonitorViewModel(_monitoringService);
                // Don't initialize monitoring yet - just set properties
                patientViewModel.PatientId = patient.Id;
                patientViewModel.FirstName = patient.FirstName;
                patientViewModel.LastName = patient.LastName;
                patientViewModel.DateOfBirth = patient.DateOfBirth;
                patientViewModel.PrimaryCareGiver = patient.PrimaryCareGiver;
                patientViewModel.RoomId = patient.RoomId;
                AllPatients.Add(patientViewModel);
            }
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
            _detailsViewModel = new PatientDetailsViewModel();
            _detailsWindow = new PatientDetailsWindow
            {
                DataContext = _detailsViewModel
            };

            if (System.Windows.Application.Current.MainWindow != null)
            {
                _detailsWindow.Owner = System.Windows.Application.Current.MainWindow;
            }

            if (PatientDoubleClicked != null)
            {
                _detailsViewModel.Patient = PatientDoubleClicked;
            }
            _detailsWindow.Show();
        };

        // Show the window
        _detailsWindow.Show();
    }

    private void UpdateDetailsWindow()
    {
        // Create window on first use if it doesn't exist
        if (_detailsWindow == null && PatientDoubleClicked != null)
        {
            InitializeDetailsWindow();
        }

        if (_detailsViewModel != null && PatientDoubleClicked != null)
        {
            _detailsViewModel.Patient = PatientDoubleClicked;
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
