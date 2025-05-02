using Application.BookingManager.Dtos;
using Application.BookingManager.Extensions;
using Application.BookingManager.Ports;
using Application.BookingManager.Responses;
using Application.BookingManager.Resquets;
using Application.PaymentManager.Dtos;
using Application.PaymentManager.Ports;
using Application.PaymentManager.Responses;
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
    private readonly IPaymentProcessorFactory _processorFactory;

    public BookingManagerPort(IBookingRepository repository,
                              IRoomRepository roomRepository,
                              IGuestRepository guestRepository,
                              IPaymentProcessorFactory processorFactory)
    {
        _repository = repository;
        _roomRepositor = roomRepository;
        _guestRepository = guestRepository;
        _processorFactory = processorFactory;
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

    public async Task<PaymentResponse> PayForBooking(PaymentRequestDto request)
    {
        try
        {
            var processor = _processorFactory.GetPaymentProcessor(request.PaymentProvider);
            var payment = await processor.CapturePaymentAsy(request.PaymentIntention);
            return payment;
        }
        catch (Exception exception)
        {
            return exception.OnPay(request.PaymentProvider);
        }
    }
}
