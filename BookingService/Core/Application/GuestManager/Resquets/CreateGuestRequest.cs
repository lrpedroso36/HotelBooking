using Application.GuestManager.Dtos;

namespace Application.GuestManager.Resquets;

public class CreateGuestRequest
{
    public GuestDto Data { get; set; } = null!;
}
