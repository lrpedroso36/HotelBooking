using Domain.GuestAggregate.Entities;
using Domain.GuestAggregate.Enums;
using Domain.GuestAggregate.ValueObjects;

namespace Application.GuestManager.Dtos;

public class GuestDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string SurName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string IdNumber { get; set; } = null!;
    public int Type { get; set; }

    public static implicit operator Guest(GuestDto guest)
    {
        return new Guest()
        {
            Id = guest.Id,
            Name = guest.Name,
            SurName = guest.SurName,
            Email = guest.Email,
            PersonDocument = new PersonDocument()
            {
                IdNumber = guest.IdNumber,
                Type = (DocumentType)guest.Type
            }
        };
    }

    public static implicit operator GuestDto(Guest guest)
    {
        return new GuestDto()
        {
            Id = guest.Id,
            Name = guest.Name,
            SurName = guest.SurName,
            Email = guest.Email,
            IdNumber = guest.PersonDocument.IdNumber,
            Type = (int)guest.PersonDocument.Type
        };
    }
}
