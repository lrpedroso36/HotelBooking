using Application.PaymentManager.Dtos;
using Application.PaymentManager.Responses;

namespace Application.PaymentManager.Ports;

public interface IMercadoPagoPaymentManagerPort
{
    Task<PaymentResponse> PayWithCreditCardAsyn(string paymentIntention);

    Task<PaymentResponse> PayWithDebitCardAsync(string paymentIntention);

    Task<PaymentResponse> PayBankTransferAsync(string paymentIntention);
}
