using Application.PaymentManager.Dtos;
using Application.Shared;

namespace Application.PaymentManager.Responses;

public class PaymentResponse : ResponseBase
{
    public PaymentStateDto Data { get; set; } = null!;
}
