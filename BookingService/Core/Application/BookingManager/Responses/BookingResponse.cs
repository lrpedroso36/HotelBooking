using Application.BookingManager.Dtos;
using Application.Shared;

namespace Application.BookingManager.Responses;

public class BookingResponse : ResponseBase
{
    public BookingDto Data { get; set; } = null!;
}
