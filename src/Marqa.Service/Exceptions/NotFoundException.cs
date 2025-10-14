namespace Marqa.Service.Exceptions;

public class NotFoundException : Exception
{
    public int StatusCode { get; set; }
    public NotFoundException(string message) : base(message)
    {
        StatusCode = 404;
    }
}
