namespace Marqa.MobileApi.Models;

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