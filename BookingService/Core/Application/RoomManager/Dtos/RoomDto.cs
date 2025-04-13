using Domain.RoomAggregate.Entities;
using Domain.RoomAggregate.Enums;
using Domain.RoomAggregate.ValueObjects;

namespace Application.RoomManager.Dtos;

public class RoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Level { get; set; }
    public bool InMaintenance { get; set; }
    public decimal Value { get; set; }
    public int Currency { get; set; }

    public static implicit operator Room(RoomDto room)
    {
        return new Room()
        {
            Id = room.Id,
            Name = room.Name,
            Level = room.Level,
            InMaintenance = room.InMaintenance,
            Prince = new CurrentPrice()
            {
                Currency = (AcceptedCurrencies)room.Currency,
                Value = room.Value
            }
        };
    }

    public static implicit operator RoomDto(Room room)
    {
        return new RoomDto()
        {
            Id = room.Id,
            Name = room.Name,
            Level = room.Level,
            InMaintenance = room.InMaintenance,
            Value = room.Prince.Value,
            Currency = (int)room.Prince.Currency
        };
    }
}
