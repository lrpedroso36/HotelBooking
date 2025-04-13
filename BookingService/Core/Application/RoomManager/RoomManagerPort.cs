using Application.GuestManager.Responses;
using Application.GuestManager.Resquets;
using Application.RoomManager.Dtos;
using Application.RoomManager.Ports;
using Application.Shared.Enumns;
using Domain.RoomAggregate.Exceptions;
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
            var guest = RoomDto.MapToEntity(request.Data);

            await guest.CreateAsync(_repository);

            request.Data.Id = guest.Id;

            return new RoomResponse()
            {
                Data = request.Data,
                Success = true
            };
        }
        catch (MinLengthException)
        {
            return ResponseError(ErrorCode.ROOM_INVALID_MIN_LENGTH, "Have one or more fields with min length.");
        }
        catch (MissingRequiredException)
        {
            return ResponseError(ErrorCode.ROOM_MISSING_REQUIRED, "Have one or more missing required fields.");
        }
        catch (CurrentPrinceException)
        {
            return ResponseError(ErrorCode.ROOM_INVALID_CURRENT_PRINCE, "Prince has invalid.");
        }
        catch (Exception)
        {
            return ResponseError(ErrorCode.ROOM_COULDNOT_STORE_DATA, "There was an error when saving to DB.");
        }
    }

    public async Task<RoomResponse> GetAsync(int id)
    {
        try
        {
            var quest = await _repository.GetAsync(id);

            if (quest == null)
                throw new NotFoundException();

            var result = RoomDto.MapToDTO(quest);
            return new RoomResponse()
            {
                Success = true,
                Data = result
            };
        }
        catch (NotFoundException)
        {
            return ResponseError(ErrorCode.ROOM_NOT_FOUND, "Room not found.");
        }
        catch (Exception)
        {
            return ResponseError(ErrorCode.ROOM_COULDNOT_STORE_DATA, "There was an error when saving to DB.");
        }
    }

    private static RoomResponse ResponseError(ErrorCode errorCode, string message)
      => new()
      {
          Success = false,
          ErrorCode = errorCode,
          Message = message
      };
}
