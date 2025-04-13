using Domain.GuestAggregate.Entities;
using Domain.GuestAggregate.Ports;

namespace Data.GuestData;

public class GuestRepository : IGuestRepository
{
    private HotelDbContext _context;

    public GuestRepository(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(Guest guest)
    {
        _context.Guests.Add(guest);
        await _context.SaveChangesAsync();
        return guest.Id;
    }

    public async Task<Guest> GetAsync(int id)
    {
        var guest = await _context.Guests.FindAsync(id);
        return guest;
    }
}
