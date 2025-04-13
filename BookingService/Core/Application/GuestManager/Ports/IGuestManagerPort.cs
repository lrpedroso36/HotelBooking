using Application.GuestManager.Responses;
using Application.GuestManager.Resquets;

namespace Application.GuestManager.Ports;

public interface IGuestManagerPort
{
    Task<GuestResponse> CreateAsync(CreateGuestRequest request);

    Task<GuestResponse> GetAsync(int id);
}
