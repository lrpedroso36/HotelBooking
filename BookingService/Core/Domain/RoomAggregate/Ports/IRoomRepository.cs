using Domain.RoomAggregate.Entities;

namespace Domain.RoomAggregate.Ports;

public interface IRoomRepository
{
    Task<Room> GetAsync(int id);
    Task<Room> GetAggregateAsync(int id);
    Task<Room> CreateAsync(Room room);
}
