using Microsoft.AspNetCore.Http;

namespace Marqa.Shared.Helpers;

public static class HttpContextHelper
{
    public static IHttpContextAccessor HttpContextAccessor { get; set; }
    public static HttpContext HttpContext => HttpContextAccessor.HttpContext;
    public static IHeaderDictionary Header => HttpContext.Response.Headers;
}