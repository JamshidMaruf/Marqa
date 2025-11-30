namespace Marqa.Service.Exceptions;

public class RequestRefusedException : Exception
{
    public int StatusCode { get; set; }
    public RequestRefusedException(string message) : base(message)
    {
        StatusCode = 409;
    }
}
