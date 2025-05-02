using Application.PaymentManager.Enums;
using Application.PaymentManager.Ports;

namespace Payment.Application.MercadoPago;

public class PaymentProcessorFactory : IPaymentProcessorFactory
{
    private readonly IEnumerable<IPaymentProcessor> _paymentProcessors;

    public PaymentProcessorFactory(IEnumerable<IPaymentProcessor> paymentProcessors)
    {
        _paymentProcessors = paymentProcessors;
    }

    public IPaymentProcessor GetPaymentProcessor(SupportedPaymentProviders provider)
    {
        var paymentProcessor = _paymentProcessors.FirstOrDefault(x => x.Provider == provider);

        if (paymentProcessor == default)
        {
            throw new NotImplementedException($"Payment selected isn't implementation '{provider}'");
        }

        return paymentProcessor;
    }
}
