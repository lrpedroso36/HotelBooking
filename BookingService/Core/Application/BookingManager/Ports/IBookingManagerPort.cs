using Application.BookingManager.Responses;
using Application.BookingManager.Resquets;

namespace Application.BookingManager.Ports;

public interface IBookingManagerPort
{
    Task<BookingResponse> CreateAsync(CreateBookingResquest request);

    Task<BookingResponse> GetAsync(int id);
}
