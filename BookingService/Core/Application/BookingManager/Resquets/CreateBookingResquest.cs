using Application.BookingManager.Dtos;

namespace Application.BookingManager.Resquets;

public class CreateBookingResquest
{
    public BookingDto Data { get; set; } = null!;
}
