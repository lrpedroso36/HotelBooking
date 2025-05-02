using Application.PaymentManager.Enums;
using System.Text.Json.Serialization;

namespace Application.PaymentManager.Dtos;


public class PaymentRequestDto
{
    [JsonIgnore]
    public int BookingId { get; set; }
    public string PaymentIntention { get; set; } = null!;
    public SupportedPaymentMethods PaymentMethod { get; set; }
    public SupportedPaymentProviders PaymentProvider { get; set; }
}
