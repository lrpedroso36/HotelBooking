using Domain.BookingAggregate.Enums;
using Domain.BookingAggregate.Exceptions;
using Domain.BookingAggregate.Ports;
using Domain.GuestAggregate.Entities;
using Domain.RoomAggregate.Entities;
using Action = Domain.BookingAggregate.Enums.Action;

namespace Domain.BookingAggregate.Entities;

public class Booking
{
    public Booking()
    {
        Status = Status.Created;
    }

    public int Id { get; set; }
    public DateTime PlaceAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public Room Room { get; set; } = null!;
    public Guest Guest { get; set; } = null!;
    private Status Status { get; set; }
    public Status CurrentStatus { get { return Status; } }
    public void ChangeState(Action action)
    {
        Status = (Status, action) switch
        {
            (Status.Created, Action.Pay) => Status.Paid,
            (Status.Created, Action.Cancel) => Status.Canceled,
            (Status.Paid, Action.Finish) => Status.Finished,
            (Status.Paid, Action.Refound) => Status.Refounded,
            (Status.Canceled, Action.Reopen) => Status.Created,
            _ => Status
        };
    }

    public async Task CreateAsync(IBookingRepository repository)
    {
        ValidateState();

        if (Id == 0)
        {
            var result = await repository.CreateAsync(this);
            Id = result;
        }
        else
        {
        }

    }

    private void ValidateState()
    {
        if (PlaceAt == default)
        {
            throw new PlaceAtInvalidInformationException();
        }

        if (Start == default)
        {
            throw new StartInvalidInformationException();
        }

        if (End == default)
        {
            throw new EndInvalidInformationException();
        }

        if (Guest.Id == 0)
        {
            throw new GuestInvalidInformationException();
        }

        if (Room.Id == 0)
        {
            throw new RoomInvalidInformationException();
        }
    }
}
