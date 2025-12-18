using WPFTest.Domain.Entities;

namespace WPFTest.Domain.Ports;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task<Room?> GetRoomByIdAsync(string id);
}
