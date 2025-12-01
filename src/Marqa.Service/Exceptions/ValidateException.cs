namespace Marqa.Service.Exceptions;
public class ValidateException : Exception
{
    public List<string> Errors { get; }
    public int StatusCode { get; }
    public ValidateException(List<string> errors)
        : base("Validation failed")
    {
        Errors = errors;
        StatusCode = 400; // Bad Request
    }
}
