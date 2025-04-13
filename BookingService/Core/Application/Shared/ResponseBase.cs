using Application.Shared.Enumns;

namespace Application.Shared;

public abstract class ResponseBase
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public ErrorCode ErrorCode { get; set; }
}
