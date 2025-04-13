namespace Domain.Shared.Exceptions;

public class DataBaseException : Exception
{
    public DataBaseException(string message)
        : base(message)
    {
    }
}
