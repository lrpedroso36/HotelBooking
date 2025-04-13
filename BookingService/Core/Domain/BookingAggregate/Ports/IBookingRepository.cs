using Domain.BookingAggregate.Entities;

namespace Domain.BookingAggregate.Ports;

public interface IBookingRepository
{

    Task<Booking?> GetAsync(int id);
    Task<int> CreateAsync(Booking guest);
}
