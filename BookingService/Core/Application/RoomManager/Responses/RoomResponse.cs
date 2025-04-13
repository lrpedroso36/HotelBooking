using Application.RoomManager.Dtos;
using Application.Shared;

namespace Application.GuestManager.Responses;

public class RoomResponse : ResponseBase
{
    public RoomDto Data { get; set; } = null!;
}
