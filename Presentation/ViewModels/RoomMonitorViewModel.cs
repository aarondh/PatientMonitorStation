using System.Collections.ObjectModel;
using WPFTest.Application.Services;
using WPFTest.Domain.Entities;

namespace WPFTest.Presentation.ViewModels;

public class RoomMonitorViewModel : ViewModelBase, IDisposable
{
    private readonly MonitoringService _monitoringService;
    private string _roomId = string.Empty;
    private string _roomName = string.Empty;

    public string RoomId
    {
        get => _roomId;
        set => SetProperty(ref _roomId, value);
    }

    public string RoomName
    {
        get => _roomName;
        set => SetProperty(ref _roomName, value);
    }

    public ObservableCollection<PatientMonitorViewModel> Patients { get; } = new();

    public RoomMonitorViewModel(MonitoringService monitoringService)
    {
        _monitoringService = monitoringService;
    }

    public async Task InitializeAsync(Room room)
    {
        RoomId = room.Id;
        RoomName = room.Name;

        var episodes = await _monitoringService.GetAllEpisodesOfCareAsync();
        var roomEpisodes = episodes.Where(e => e.RoomId == room.Id);

        foreach (var episode in roomEpisodes)
        {
            var patientViewModel = new PatientMonitorViewModel(_monitoringService);
            patientViewModel.Initialize(episode);
            Patients.Add(patientViewModel);
        }
    }

    public void Dispose()
    {
        foreach (var patient in Patients)
        {
            patient.Dispose();
        }
        Patients.Clear();
    }
}
