using Application.BookingManager.Dtos;
using Application.BookingManager.Extensions;
using Application.BookingManager.Ports;
using Application.BookingManager.Responses;
using Application.BookingManager.Resquets;
using Domain.BookingAggregate.Entities;
using Domain.BookingAggregate.Ports;
using Domain.GuestAggregate.Ports;
using Domain.RoomAggregate.Ports;
using Domain.Shared.Exceptions;

namespace Application.BookingManager;

public class BookingManagerPort : IBookingManagerPort
{
    private readonly IBookingRepository _repository;
    private readonly IRoomRepository _roomRepositor;
    private readonly IGuestRepository _guestRepository;

    public BookingManagerPort(IBookingRepository repository,
                              IRoomRepository roomRepository,
                              IGuestRepository guestRepository)
    {
        _repository = repository;
        _roomRepositor = roomRepository;
        _guestRepository = guestRepository;
    }

    public async Task<BookingResponse> CreateAsync(CreateBookingResquest request)
    {

        try
        {
            Booking booking = request.Data;
            booking.Guest = await _guestRepository.GetAsync(request.Data.GuestId);
            booking.Room = await _roomRepositor.GetAggregateAsync(request.Data.RoomId);

            await booking.CreateAsync(_repository);
            request.Data.Id = booking.Id;

            return new BookingResponse()
            {
                Data = request.Data,
                Success = true
            };
        }
        catch (Exception exception)
        {
            return exception.OnCreate();
        }
    }

    public async Task<BookingResponse> GetAsync(int id)
    {
        try
        {
            var booking = await _repository.GetAsync(id);

            if (booking == null)
                throw new NotFoundException();

            BookingDto result = booking;
            return new BookingResponse()
            {
                Success = true,
                Data = result
            };
        }
        catch (Exception excetion)
        {
            return excetion.OnGet();
        }
    }
}
