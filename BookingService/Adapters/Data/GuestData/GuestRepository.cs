using Domain.GuestAggregate.Entities;
using Domain.GuestAggregate.Ports;
using Domain.Shared.Exceptions;

namespace Data.GuestData;

public class GuestRepository : IGuestRepository
{
    private readonly HotelDbContext _context;

    public GuestRepository(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<Guest> CreateAsync(Guest guest)
    {
        try
        {
            _context.Guests.Add(guest);
            var result = await _context.SaveChangesAsync();
            guest.Id = result;
            return guest;
        }
        catch (Exception exception)
        {
            throw new DataBaseException(exception.Message);
        }

    }

    public async Task<Guest> GetAsync(int id)
    {
        try
        {
            var guest = await _context.Guests.FindAsync(id);
            return guest;
        }
        catch (Exception exception)
        {
            throw new DataBaseException(exception.Message);
        }
    }
}
