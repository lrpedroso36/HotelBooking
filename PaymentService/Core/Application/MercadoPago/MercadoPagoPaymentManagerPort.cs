using Application.PaymentManager.Dtos;
using Application.PaymentManager.Enums;
using Application.PaymentManager.Ports;
using Application.PaymentManager.Responses;
using Payment.Application.MercadoPago.Exceptions;
using ApplicationSharedEnumns = Application.Shared.Enumns;

namespace Payment.Application.MercadoPago;

public class MercadoPagoPaymentManagerPort : IMercadoPagoPaymentManagerPort
{
    public Task<PaymentResponse> PayBankTransferAsync(string paymentIntention)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentResponse> PayWithCreditCardAsyn(string paymentIntention)
    {
        try
        {
            if (string.IsNullOrEmpty(paymentIntention))
            {
                throw new PaymentIntentionInvalidException();
            }

            var result = new PaymentStateDto()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.Now,
                Status = PaymentStatus.Success,
                Message = $"Successfully paid {paymentIntention}."
            };

            var response = new PaymentResponse() { Success = true, Data = result };
            return Task.FromResult(response);
        }
        catch (PaymentIntentionInvalidException)
        {
            var response = new PaymentResponse()
            {
                ErrorCode = ApplicationSharedEnumns.ErrorCode.PAYMENT_INVALID_PAIMENT_INTENTION,
                Message = "PaymentIntention has invalid."
            };

            return Task.FromResult(response);
        }
        catch (Exception exception)
        {
            var response = new PaymentResponse()
            {
                ErrorCode = ApplicationSharedEnumns.ErrorCode.GENERAL_ERROR,
                Message = exception.Message
            };

            return Task.FromResult(response);
        }
    }

    public Task<PaymentResponse> PayWithDebitCardAsync(string paymentIntention)
    {
        throw new NotImplementedException();
    }
}
