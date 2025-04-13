using Application.GuestManager.Dtos;
using Application.GuestManager.Ports;
using Application.GuestManager.Responses;
using Application.GuestManager.Resquets;
using Application.Shared.Enumns;
using Domain.GuestAggregate.Entities;
using Domain.GuestAggregate.Exceptions;
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
        catch (InvalidaEmailException)
        {
            return ResponseError(ErrorCode.GUEST_INVALID_EMAIL, "Email has invalid.");
        }
        catch (MinLengthException)
        {
            return ResponseError(ErrorCode.GUEST_INVALID_MIN_LENGTH, "Have one or more fields with min length.");
        }
        catch (MissingRequiredException)
        {
            return ResponseError(ErrorCode.GUEST_MISSING_REQUIRED, "Have one or more missing required fields.");
        }
        catch (PersonDocumentException)
        {
            return ResponseError(ErrorCode.GUEST_INVALID_PERSON_DOCUMENT, "Person document has invalid.");
        }
        catch (Exception)
        {
            return ResponseError(ErrorCode.GUEST_COULDNOT_STORE_DATA, "There was an error when saving to DB.");
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
        catch (NotFoundException)
        {
            return ResponseError(ErrorCode.GUEST_NOT_FOUND, "Guest not found.");
        }
        catch (Exception)
        {
            return ResponseError(ErrorCode.GUEST_COULDNOT_STORE_DATA, "There was an error when saving to DB.");
        }
    }

    private static GuestResponse ResponseError(ErrorCode errorCode, string message)
      => new()
      {
          Success = false,
          ErrorCode = errorCode,
          Message = message
      };
}
