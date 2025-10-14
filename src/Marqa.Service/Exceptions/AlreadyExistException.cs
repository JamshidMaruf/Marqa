namespace Marqa.Service.Exceptions;

public class AlreadyExistException : Exception
{
    public int StatusCode { get; set; }
    public AlreadyExistException(string message) : base(message)
    {
        StatusCode = 403;
    }
}
