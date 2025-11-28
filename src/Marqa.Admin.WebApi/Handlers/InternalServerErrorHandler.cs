using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Handlers;

public class InternalServerErrorHandler(ILogger<InternalServerErrorHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var ex = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError, 
            Title = exception.Message,
        };
        
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(ex, cancellationToken);
        
        logger.LogError(exception.Message);

        return true;
    }
}