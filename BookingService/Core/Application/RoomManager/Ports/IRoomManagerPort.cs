using Application.GuestManager.Responses;
using Application.GuestManager.Resquets;

namespace Application.RoomManager.Ports;

public interface IRoomManagerPort
{
    Task<RoomResponse> CreateAsync(CreateRoomRequest request);

    Task<RoomResponse> GetAsync(int id);
}
