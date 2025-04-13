using Application.GuestManager.Responses;
using Application.Shared.Enumns;
using Domain.GuestAggregate.Exceptions;
using Domain.Shared.Exceptions;

namespace Application.GuestManager.Extensions;

public static class DomainExceptionExtesions
{
    public static GuestResponse OnCreate(this Exception exception)
    {
        if (exception is InvalidaEmailException)
        {
            return ResponseError(ErrorCode.GUEST_INVALID_EMAIL, "Email has invalid.");
        }

        if (exception is MinLengthException)
        {
            return ResponseError(ErrorCode.GUEST_INVALID_MIN_LENGTH, "Have one or more fields with min length.");
        }

        if (exception is MissingRequiredException)
        {
            return ResponseError(ErrorCode.GUEST_MISSING_REQUIRED, "Have one or more missing required fields.");
        }

        if (exception is PersonDocumentException)
        {
            return ResponseError(ErrorCode.GUEST_INVALID_PERSON_DOCUMENT, "Person document has invalid.");
        }

        if (exception is DataBaseException)
        {
            return ResponseError(ErrorCode.GUEST_COULDNOT_STORE_DATA, $"There was an error when saving to DB: ${exception.Message}");
        }

        return ResponseError(ErrorCode.GENERAL_ERROR, exception.Message);
    }

    public static GuestResponse OnGet(this Exception exception)
    {
        if (exception is NotFoundException)
        {
            return ResponseError(ErrorCode.GUEST_NOT_FOUND, "Guest not found.");
        }

        if (exception is DataBaseException)
        {
            return ResponseError(ErrorCode.GUEST_COULDNOT_GET_DATA, $"There was an error when geting to DB: ${exception.Message}");
        }

        return ResponseError(ErrorCode.GENERAL_ERROR, exception.Message);
    }

    private static GuestResponse ResponseError(ErrorCode errorCode, string message)
      => new()
      {
          Success = false,
          ErrorCode = errorCode,
          Message = message
      };

}
