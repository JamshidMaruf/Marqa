using Marqa.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Handlers;

public class RequestRefusedExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not RequestRefusedException requestRefusedException)
            return false;
        
        var ex = new ProblemDetails
        {
            Status = requestRefusedException.StatusCode, 
            Title = exception.Message,
        };
            
        httpContext.Response.StatusCode = requestRefusedException.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(ex, cancellationToken);
        return true;
    }
}