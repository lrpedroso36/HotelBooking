using Application.BookingManager.Responses;
using Application.Shared.Enumns;
using Domain.BookingAggregate.Exceptions;
using Domain.Shared.Exceptions;

namespace Application.BookingManager.Extensions;

public static class DomainExceptionExtesions
{

    public static BookingResponse OnCreate(this Exception exception)
    {
        if (exception is PlaceAtInvalidInformationException)
        {
            return ResponseError(ErrorCode.BOOKING_INVALID_PLACE_AT, "The place at invalid.");
        }

        if (exception is StartInvalidInformationException)
        {
            return ResponseError(ErrorCode.BOOKING_INVALID_START, "The start invalid.");
        }

        if (exception is EndInvalidInformationException)
        {
            return ResponseError(ErrorCode.BOOKING_INVALID_END, "The end invalid.");
        }

        if (exception is GuestInvalidInformationException)
        {
            return ResponseError(ErrorCode.BOOKING_INVALID_GUEST, "Guest is required.");
        }

        if (exception is RoomInvalidInformationException)
        {
            return ResponseError(ErrorCode.BOOKING_INVALID_ROOM, "Room is required.");
        }

        if (exception is RoomCannotBeBookedException)
        {
            return ResponseError(ErrorCode.BOOKING_CANNOT_BE_BOOKING, "The selected room is unavailable.");
        }

        if (exception is DataBaseException)
        {
            return ResponseError(ErrorCode.BOOKING_COULDNOT_STORE_DATA, $"There was an error when saving to DB {exception.Message}.");
        }

        return ResponseError(ErrorCode.GENERAL_ERROR, exception.Message);
    }

    public static BookingResponse OnGet(this Exception exception)
    {
        if (exception is NotFoundException)
        {
            return ResponseError(ErrorCode.BOOKING_NOT_FOUND, "Booking not found.");
        }

        if (exception is DataBaseException)
        {
            return ResponseError(ErrorCode.BOOKING_COULDNOT_GET_DATA, $"There was an error when geting to DB {exception.Message}.");
        }

        return ResponseError(ErrorCode.GENERAL_ERROR, exception.Message);
    }

    private static BookingResponse ResponseError(ErrorCode errorCode, string message)
        => new()
        {
            Success = false,
            ErrorCode = errorCode,
            Message = message
        };
}
