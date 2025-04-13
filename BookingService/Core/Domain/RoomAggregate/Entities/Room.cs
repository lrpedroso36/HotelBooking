using Domain.RoomAggregate.Exceptions;
using Domain.RoomAggregate.Ports;
using Domain.RoomAggregate.ValueObjects;
using Domain.Shared.Exceptions;

namespace Domain.RoomAggregate.Entities;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public bool InMaintenance { get; set; }
    public CurrentPrice Prince { get; set; }

    public bool IsAvaible
    {
        get
        {
            return !(InMaintenance || HasGuest);
        }
    }

    public bool HasGuest
    {
        get { return true; }
    }

    private void ValidateState()
    {
        if (string.IsNullOrEmpty(Name))
        {
            throw new MissingRequiredException();
        }

        if (Name.Length <= 3)
        {
            throw new MinLengthException();
        }

        if (Prince == null ||
            Prince.Value == 0)
        {
            throw new CurrentPrinceException();
        }
    }

    public async Task CreateAsync(IRoomRepository repository)
    {
        ValidateState();

        if (Id == 0)
        {
            Id = await repository.CreateAsync(this);
        }
    }
}
