using Application.GuestManager.Dtos;
using Application.GuestManager.Extensions;
using Application.GuestManager.Ports;
using Application.GuestManager.Responses;
using Application.GuestManager.Resquets;
using Domain.GuestAggregate.Entities;
using Domain.GuestAggregate.Ports;
using Domain.Shared.Exceptions;

namespace Application.GuestManager;

public class GuestManagerPort : IGuestManagerPort
{
    private readonly IGuestRepository _repository;

    public GuestManagerPort(IGuestRepository repository)
    {
        _repository = repository;
    }

    public async Task<GuestResponse> CreateAsync(CreateGuestRequest request)
    {
        try
        {
            Guest guest = request.Data;
            await guest.CreateAsync(_repository);
            request.Data.Id = guest.Id;

            return new GuestResponse()
            {
                Data = request.Data,
                Success = true
            };
        }
        catch (Exception exception)
        {
            return exception.OnCreate();
        }
    }

    public async Task<GuestResponse> GetAsync(int id)
    {
        try
        {
            var quest = await _repository.GetAsync(id);

            if (quest == null)
                throw new NotFoundException();

            GuestDto result = quest;
            return new GuestResponse()
            {
                Success = true,
                Data = result
            };
        }
        catch (Exception exception)
        {
            return exception.OnGet();
        }
    }
}
