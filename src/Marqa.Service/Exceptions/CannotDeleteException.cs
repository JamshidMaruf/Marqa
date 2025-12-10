namespace Marqa.Service.Exceptions;

public class CannotDeleteException : Exception
{
    public CannotDeleteException(string message) : base(message)
    {
    }

    public CannotDeleteException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}