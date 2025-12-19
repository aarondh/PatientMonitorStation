using WPFPatientMonitor.Domain.Entities;

namespace WPFPatientMonitor.Domain.Ports;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task<Room?> GetRoomByIdAsync(string id);
}
