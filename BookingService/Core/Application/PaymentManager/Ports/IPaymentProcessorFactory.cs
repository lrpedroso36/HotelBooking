using Application.PaymentManager.Enums;

namespace Application.PaymentManager.Ports;

public interface IPaymentProcessorFactory
{
    IPaymentProcessor GetPaymentProcessor(SupportedPaymentProviders provider);
}
