using Application.PaymentManager.Enums;
using Application.PaymentManager.Responses;

namespace Application.PaymentManager.Ports;

public interface IPaymentProcessor
{
    public SupportedPaymentProviders Provider { get; set; }
    Task<PaymentResponse> CapturePaymentAsy(string paymentIntention);
}
