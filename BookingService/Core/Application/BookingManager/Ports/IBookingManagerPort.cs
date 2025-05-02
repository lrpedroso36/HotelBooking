using Application.BookingManager.Responses;
using Application.BookingManager.Resquets;
using Application.PaymentManager.Dtos;
using Application.PaymentManager.Responses;

namespace Application.BookingManager.Ports;

public interface IBookingManagerPort
{
    Task<BookingResponse> CreateAsync(CreateBookingResquest request);

    Task<BookingResponse> GetAsync(int id);

    Task<PaymentResponse> PayForBooking(PaymentRequestDto request);
}
