using WPFTest.Domain.Entities;

namespace WPFTest.Domain.Ports;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllPatientsAsync();
    Task<Patient?> GetPatientByIdAsync(string patientId);
    Task<IEnumerable<Patient>> GetPatientsByRoomIdAsync(string roomId);

    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task<Room?> GetRoomByIdAsync(string roomId);

    Task<IEnumerable<MonitorProfile>> GetAllMonitorProfilesAsync();
    Task<MonitorProfile?> GetMonitorProfileByIdAsync(string profileId);

    Task<IEnumerable<EpisodeOfCare>> GetAllEpisodesOfCareAsync();
    Task<EpisodeOfCare?> GetEpisodeOfCareByRoomIdAsync(string roomId);
}
