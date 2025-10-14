namespace Marqa.WebApi.Models;

public class Response<TModel>
{
    public int Status { get; set; }
    public string Message { get; set; }
    public TModel Data { get; set; }
}

public class Response
{
    public int Status { get; set; }
    public string Message { get; set; }
}