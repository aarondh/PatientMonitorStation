using WPFTest.Domain.Entities;

namespace WPFTest.Domain.Ports;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllPatientsAsync();
    Task<Patient?> GetPatientByIdAsync(string patientId);
    Task<IEnumerable<Patient>> GetPatientsByRoomIdAsync(string roomId);
}
