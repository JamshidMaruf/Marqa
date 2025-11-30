namespace Marqa.Shared.Models;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
}
