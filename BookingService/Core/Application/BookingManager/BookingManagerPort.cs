using Application.BookingManager.Dtos;
using Application.BookingManager.Ports;
using Application.BookingManager.Responses;
using Application.BookingManager.Resquets;
using Domain.BookingAggregate.Entities;
using Domain.BookingAggregate.Ports;

namespace Application.BookingManager;

public class BookingManagerPort : IBookingManagerPort
{
    private readonly IBookingRepository _repository;

    public BookingManagerPort(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<BookingResponse> CreateAsync(CreateBookingResquest request)
    {

        try
        {
            Booking booking = request.Data;

            await booking.CreateAsync(_repository);
            request.Data.Id = booking.Id;

            return new BookingResponse()
            {
                Data = request.Data,
                Success = true
            };
        }
        catch (Exception)
        {

            throw;
        }
    }

    public Task<BookingDto> GetAsync(int id)
    {
        throw new NotImplementedException();
    }
}
