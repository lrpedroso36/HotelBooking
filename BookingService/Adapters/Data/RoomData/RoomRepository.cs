using Domain.RoomAggregate.Entities;
using Domain.RoomAggregate.Ports;

namespace Data.RoomData;

public class RoomRepository : IRoomRepository
{
    private HotelDbContext _context;

    public RoomRepository(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return room.Id;
    }

    public async Task<Room> GetAsync(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        return room;
    }
}
