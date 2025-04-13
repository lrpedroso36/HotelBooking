using Domain.GuestAggregate.Enums;

namespace Domain.GuestAggregate.ValueObjects;

public class PersonDocument
{
    public string IdNumber { get; set; }
    public DocumentType Type { get; set; }
}
