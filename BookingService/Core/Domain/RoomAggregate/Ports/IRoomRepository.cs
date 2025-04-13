using Domain.RoomAggregate.Entities;

namespace Domain.RoomAggregate.Ports;

public interface IRoomRepository
{
    Task<Room> GetAsync(int id);
    Task<int> CreateAsync(Room room);
}
