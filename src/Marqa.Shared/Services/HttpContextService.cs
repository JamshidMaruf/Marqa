using Microsoft.AspNetCore.Http;

namespace Marqa.Shared.Services;

public class HttpContextService : IHttpContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public HttpContext? HttpContext => _httpContextAccessor.HttpContext;

    public IHeaderDictionary? ResponseHeaders => HttpContext?.Response.Headers;

    public void AddResponseHeader(string key, string value)
    {
        if (HttpContext?.Response.Headers != null)
        {
            HttpContext.Response.Headers.Append(key, value);
        }
    }
    
    public void RemoveResponseHeader(string key)
    {
        if (HttpContext?.Response.Headers != null)
        {
            HttpContext.Response.Headers.Remove(key);
        }
    }
}

