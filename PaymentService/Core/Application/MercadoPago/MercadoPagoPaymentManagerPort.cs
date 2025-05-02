using Application.PaymentManager.Dtos;
using Application.PaymentManager.Enums;
using Application.PaymentManager.Exceptions;
using Application.PaymentManager.Ports;
using Application.PaymentManager.Responses;
using ApplicationSharedEnumns = Application.Shared.Enumns;

namespace Payment.Application.MercadoPago;

public class MercadoPagoPaymentManagerPort : IPaymentProcessor
{
    public SupportedPaymentProviders Provider { get; set; } = SupportedPaymentProviders.MercadoPago;

    public Task<PaymentResponse> CapturePaymentAsy(string paymentIntention)
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
}
