namespace Marqa.Service.Exceptions;

public class NotMatchedException : Exception
{
    public int StatusCode { get; set; }
    public NotMatchedException(string message) : base(message)
    {
        StatusCode = 401;
    }
}