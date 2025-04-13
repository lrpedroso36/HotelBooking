using Domain.RoomAggregate.Enums;

namespace Domain.RoomAggregate.ValueObjects;

public class CurrentPrice
{
    public decimal Value { get; set; }

    public AcceptedCurrencies Currency { get; set; }
}
