using Application.PaymentManager.Enums;

namespace Application.PaymentManager.Dtos;

public class PaymentStateDto
{
    public PaymentStatus Status { get; set; }
    public string Id { get; set; } = null!;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string Message { get; set; } = null!;
}
