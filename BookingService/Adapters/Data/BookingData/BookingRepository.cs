using Domain.BookingAggregate.Entities;
using Domain.BookingAggregate.Ports;
using Domain.RoomAggregate.Entities;

namespace Data.BookingData;

public class BookingRepository : IBookingRepository
{
    private readonly HotelDbContext _context;

    public BookingRepository(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(Booking booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return booking.Id;
    }

    public async Task<Booking?> GetAsync(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        return booking;
    }
}
