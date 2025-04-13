using Domain.GuestAggregate.Entities;

namespace Domain.GuestAggregate.Ports;

public interface IGuestRepository
{
    Task<Guest> GetAsync(int id);
    Task<Guest> CreateAsync(Guest guest);
}
