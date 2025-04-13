using Domain.RoomAggregate.Entities;
using Domain.RoomAggregate.Ports;
using Domain.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Data.RoomData;

public class RoomRepository : IRoomRepository
{
    private readonly HotelDbContext _context;

    public RoomRepository(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<Room> CreateAsync(Room room)
    {
        try
        {
            _context.Rooms.Add(room);
            var result = await _context.SaveChangesAsync();
            room.Id = result;
            return room;
        }
        catch (Exception exception)
        {
            throw new DataBaseException(exception.Message);
        }
    }

    public async Task<Room> GetAggregateAsync(int id)
    {
        try
        {
            var room = await _context.Rooms
                        .Include(x => x.Bookings)
                        .Where(x => x.Id == id).FirstOrDefaultAsync();

            return room;
        }
        catch (Exception exception)
        {
            throw new DataBaseException(exception.Message);
        }
    }

    public async Task<Room> GetAsync(int id)
    {
        try
        {
            var room = await _context.Rooms
                        .Where(x => x.Id == id).FirstOrDefaultAsync();

            return room;
        }
        catch (Exception exception)
        {
            throw new DataBaseException(exception.Message);
        }
    }
}
