using Marqa.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Handlers;

public class NotMatchedExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not NotMatchedException notMatchedException)
            return false;
        
        var ex = new ProblemDetails
        {
            Status = notMatchedException.StatusCode, 
            Title = exception.Message,
        };
            
        httpContext.Response.StatusCode = notMatchedException.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(ex, cancellationToken);
        return true;
    }
}
