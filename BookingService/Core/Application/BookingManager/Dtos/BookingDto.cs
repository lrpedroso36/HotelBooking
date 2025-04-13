using Application.GuestManager.Dtos;
using Domain.BookingAggregate.Entities;
using Domain.BookingAggregate.Enums;
using Domain.GuestAggregate.Entities;
using Domain.GuestAggregate.ValueObjects;
using Domain.RoomAggregate.Entities;

namespace Application.BookingManager.Dtos;

public class BookingDto
{
    public BookingDto()
    {
        PlaceAt = DateTime.UtcNow;
    }

    public int Id { get; set; }
    public DateTime PlaceAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int RoomId { get; set; }
    public int GuestId { get; set; }
    public Status Status { get; set; }

    public static implicit operator Booking(BookingDto booking)
    {
        return new Booking()
        {
            Id = booking.Id,
            Start = booking.Start,
            End = booking.End,
            Room = new Room() { Id = booking.RoomId },
            Guest = new Guest() { Id = booking.GuestId }
        };
    }

    public static implicit operator BookingDto(Booking booking)
    {
        return new BookingDto()
        {
            Id = booking.Id,
            PlaceAt = booking.PlaceAt,
            Start = booking.Start,
            End = booking.End,
            RoomId = booking.Room.Id,
            GuestId = booking.Guest.Id
        };
    }
}
