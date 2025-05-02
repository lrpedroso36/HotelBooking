using Application.PaymentManager.Enums;
using Application.PaymentManager.Exceptions;
using Application.PaymentManager.Responses;
using Application.Shared.Enumns;

namespace Application.BookingManager.Extensions;

public static class PaymentExceptionsExtensions
{
    public static PaymentResponse OnPay(this Exception exception, SupportedPaymentProviders provider)
    {
        if (exception is NotImplementedException)
        {
            return ResponseError(ErrorCode.PAYMENT_PROVIDER_NOT_IMPLEMENTATION, $"Payment selected isn't implementation '${provider}'");
        }

        if(exception is PaymentIntentionInvalidException)
        {
            return ResponseError(ErrorCode.PAYMENT_INVALID_PAIMENT_INTENTION, "PaymentIntention has invalid.");
        }

        return ResponseError(ErrorCode.GENERAL_ERROR, exception.Message);
    }

    private static PaymentResponse ResponseError(ErrorCode errorCode, string message)
        => new()
        {
            Success = false,
            ErrorCode = errorCode,
            Message = message
        };
}
