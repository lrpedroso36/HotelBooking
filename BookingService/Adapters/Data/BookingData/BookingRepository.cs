using Domain.BookingAggregate.Entities;
using Domain.BookingAggregate.Ports;
using Domain.Shared.Exceptions;

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
        try
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking.Id;
        }
        catch (Exception exception)
        {
            throw new DataBaseException(exception.Message);
        }
    }

    public async Task<Booking?> GetAsync(int id)
    {
        try
        {
            var booking = await _context.Bookings.FindAsync(id);
            return booking;
        }
        catch (Exception exception)
        {
            throw new DataBaseException(exception.Message);
        }
    }
}
