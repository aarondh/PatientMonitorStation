using WPFTest.Domain.Entities;
using WPFTest.Domain.Ports;

namespace WPFTest.Application.Services;

public class MonitoringService
{
    private readonly IEcgMonitor _heartRateMonitor;
    private readonly IBloodPressureMonitor _bloodPressureMonitor;
    private readonly IRespiratoryMonitor _respiratoryMonitor;
    private readonly IPulseOximetryMonitor _pulseOximetryMonitor;
    private readonly IRoomRepository _roomRepository;
    private readonly IPatientRepository _patientRepository;

    public MonitoringService(
        IEcgMonitor heartRateMonitor,
        IBloodPressureMonitor bloodPressureMonitor,
        IRespiratoryMonitor respiratoryMonitor,
        IPulseOximetryMonitor pulseOximetryMonitor,
        IRoomRepository roomRepository,
        IPatientRepository patientRepository)
    {
        _heartRateMonitor = heartRateMonitor;
        _bloodPressureMonitor = bloodPressureMonitor;
        _respiratoryMonitor = respiratoryMonitor;
        _pulseOximetryMonitor = pulseOximetryMonitor;
        _roomRepository = roomRepository;
        _patientRepository = patientRepository;
    }

    public async Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        return await _roomRepository.GetAllRoomsAsync();
    }

    public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        return await _patientRepository.GetAllPatientsAsync();
    }

    public async Task<IEnumerable<Patient>> GetPatientsByRoomIdAsync(string roomId)
    {
        return await _patientRepository.GetPatientsByRoomIdAsync(roomId);
    }

    public async Task<IEnumerable<EpisodeOfCare>> GetAllEpisodesOfCareAsync()
    {
        return await _patientRepository.GetAllEpisodesOfCareAsync();
    }

    public async Task<EpisodeOfCare?> GetEpisodeOfCareByRoomIdAsync(string roomId)
    {
        return await _patientRepository.GetEpisodeOfCareByRoomIdAsync(roomId);
    }

    public async Task<IEnumerable<MonitorProfile>> GetAllMonitorProfilesAsync()
    {
        return await _patientRepository.GetAllMonitorProfilesAsync();
    }

    public async Task<MonitorProfile?> GetMonitorProfileByIdAsync(string profileId)
    {
        return await _patientRepository.GetMonitorProfileByIdAsync(profileId);
    }

    public IObservable<EcgReading> MonitorHeartRate(string patientId)
    {
        _heartRateMonitor.StartMonitoring(patientId);
        return _heartRateMonitor.GetHeartRateStream(patientId);
    }

    public IObservable<BloodPressureReading> MonitorBloodPressure(string patientId)
    {
        _bloodPressureMonitor.StartMonitoring(patientId);
        return _bloodPressureMonitor.GetBloodPressureStream(patientId);
    }

    public IObservable<RespiratoryReading> MonitorRespiratory(string patientId)
    {
        _respiratoryMonitor.StartMonitoring(patientId);
        return _respiratoryMonitor.GetRespiratoryStream(patientId);
    }

    public IObservable<PulseOximetryReading> MonitorPulseOximetry(string patientId)
    {
        _pulseOximetryMonitor.StartMonitoring(patientId);
        return _pulseOximetryMonitor.GetPulseOximetryStream(patientId);
    }

    public void StopMonitoringPatient(string patientId)
    {
        _heartRateMonitor.StopMonitoring(patientId);
        _bloodPressureMonitor.StopMonitoring(patientId);
        _respiratoryMonitor.StopMonitoring(patientId);
        _pulseOximetryMonitor.StopMonitoring(patientId);
    }
}
