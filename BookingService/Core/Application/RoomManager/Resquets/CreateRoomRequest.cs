using Application.RoomManager.Dtos;

namespace Application.GuestManager.Resquets;

public class CreateRoomRequest
{
    public RoomDto Data { get; set; } = null!;
}
