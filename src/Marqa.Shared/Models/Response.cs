namespace Marqa.Shared.Models;

public class Response<TModel>
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public TModel Data { get; set; }
}

public class Response
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
}

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
}

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = "";
    public T Data { get; set; }

    public ApiResponse(T data, int statusCode = 200, string message = "success")
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }
}