using Application.GuestManager.Dtos;
using Application.Shared;

namespace Application.GuestManager.Responses;

public class GuestResponse : ResponseBase
{
    public GuestDto Data { get; set; } = null!;
}
