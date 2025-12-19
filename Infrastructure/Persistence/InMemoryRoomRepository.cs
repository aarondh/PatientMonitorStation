using WPFPatientMonitor.Domain.Entities;
using WPFPatientMonitor.Domain.Ports;

namespace WPFPatientMonitor.Infrastructure.Persistence;

public class InMemoryRoomRepository : IRoomRepository
{
    private readonly List<Room> _rooms = new()
    {
        new Room("room-1", "ICU 101"),
        new Room("room-2", "ICU 102"),
        new Room("room-3", "ICU 103"),
        new Room("room-4", "ICU 104"),
        new Room("room-5", "ICU 105"),
        new Room("room-6", "ICU 106"),
        new Room("room-7", "Ward 201"),
        new Room("room-8", "Ward 202"),
        new Room("room-9", "Ward 203"),
        new Room("room-10", "Ward 204"),
        new Room("room-11", "Ward 205"),
        new Room("room-12", "Ward 206")
    };

    public Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        return Task.FromResult<IEnumerable<Room>>(_rooms);
    }

    public Task<Room?> GetRoomByIdAsync(string id)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(room);
    }
}
