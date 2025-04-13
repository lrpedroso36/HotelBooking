using Application.GuestManager.Responses;
using Application.GuestManager.Resquets;
using Application.RoomManager.Dtos;
using Application.RoomManager.Extensions;
using Application.RoomManager.Ports;
using Domain.RoomAggregate.Entities;
using Domain.RoomAggregate.Ports;
using Domain.Shared.Exceptions;

namespace Application.RoomManager;

public class RoomManagerPort : IRoomManagerPort
{
    private readonly IRoomRepository _repository;

    public RoomManagerPort(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<RoomResponse> CreateAsync(CreateRoomRequest request)
    {
        try
        {
            Room room = request.Data;
            await room.CreateAsync(_repository);
            request.Data.Id = room.Id;

            return new RoomResponse()
            {
                Data = request.Data,
                Success = true
            };
        }
        catch (Exception excetion)
        {
            return excetion.OnCreate();
        }
    }

    public async Task<RoomResponse> GetAsync(int id)
    {
        try
        {
            var quest = await _repository.GetAsync(id);

            if (quest == null)
                throw new NotFoundException();

            RoomDto result = quest;
            return new RoomResponse()
            {
                Success = true,
                Data = result
            };
        }
        catch (Exception excetion)
        {
            return excetion.OnGet();
        }
    }
}
