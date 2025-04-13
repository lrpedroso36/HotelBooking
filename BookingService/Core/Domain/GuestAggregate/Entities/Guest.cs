using Domain.GuestAggregate.Exceptions;
using Domain.GuestAggregate.Ports;
using Domain.GuestAggregate.ValueObjects;
using Domain.Shared;
using Domain.Shared.Exceptions;

namespace Domain.GuestAggregate.Entities;

public class Guest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string SurName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public PersonDocument PersonDocument { get; set; } = null!;

    private void ValidateState()
    {
        if (string.IsNullOrEmpty(Name) ||
           string.IsNullOrEmpty(SurName) ||
           string.IsNullOrEmpty(Email))
        {
            throw new MissingRequiredException();
        }

        if (Name.Length <= 3 ||
            SurName.Length <= 3 ||
            Email.Length <= 3)
        {
            throw new MinLengthException();
        }

        if (!Utils.ValidateEmail(Email))
        {
            throw new InvalidaEmailException();
        }

        if (PersonDocument == null ||
           PersonDocument.IdNumber.Length <= 3 ||
           PersonDocument.Type == 0)
        {
            throw new PersonDocumentException();
        }
    }

    public async Task CreateAsync(IGuestRepository repository)
    {
        ValidateState();

        if (Id == 0)
        {
            Id = await repository.CreateAsync(this);
        }
    }
}
