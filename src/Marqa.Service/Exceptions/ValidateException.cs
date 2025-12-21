namespace Marqa.Service.Exceptions;
public class ValidateException(List<string> errors) : Exception("Validation failed")
{
    public List<string> Errors { get; } = errors;
    public int StatusCode { get; } = 400; // Bad Request
}
