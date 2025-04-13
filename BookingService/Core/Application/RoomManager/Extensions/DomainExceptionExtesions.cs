using Application.GuestManager.Responses;
using Application.Shared.Enumns;
using Domain.RoomAggregate.Exceptions;
using Domain.Shared.Exceptions;

namespace Application.RoomManager.Extensions;

public static class DomainExceptionExtesions
{
    public static RoomResponse OnCreate(this Exception exception)
    {
        if (exception is MinLengthException)
        {
            return ResponseError(ErrorCode.ROOM_INVALID_MIN_LENGTH, "Have one or more fields with min length.");
        }

        if (exception is MissingRequiredException)
        {
            return ResponseError(ErrorCode.ROOM_MISSING_REQUIRED, "Have one or more missing required fields.");
        }

        if (exception is CurrentPrinceException)
        {
            return ResponseError(ErrorCode.ROOM_INVALID_CURRENT_PRINCE, "Prince has invalid.");

        }

        if (exception is DataBaseException)
        {
            return ResponseError(ErrorCode.ROOM_COULDNOT_STORE_DATA, $"There was an error when saving to DB {exception.Message}.");
        }

        return ResponseError(ErrorCode.GENERAL_ERROR, exception.Message);
    }

    public static RoomResponse OnGet(this Exception exception)
    {
        if (exception is NotFoundException)
        {
            return ResponseError(ErrorCode.ROOM_NOT_FOUND, "Room not found.");
        }

        if (exception is DataBaseException)
        {
            return ResponseError(ErrorCode.ROOM_COULDNOT_GET_DATA, $"There was an error when geting to DB: {exception.Message}");
        }

        return ResponseError(ErrorCode.GENERAL_ERROR, exception.Message);
    }

    private static RoomResponse ResponseError(ErrorCode errorCode, string message)
        => new()
        {
            Success = false,
            ErrorCode = errorCode,
            Message = message
        };
}
