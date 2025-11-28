using Marqa.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Handlers;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {   
        if (exception is not NotFoundException notFoundException)
            return false;
        
        var ex = new ProblemDetails
        {
            Status = notFoundException.StatusCode, 
            Title = exception.Message,
        };
        
        httpContext.Response.StatusCode = notFoundException.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(ex, cancellationToken);
        return true;
    }
}