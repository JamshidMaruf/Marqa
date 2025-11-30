using Marqa.Service.Exceptions;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Handlers;

public class AlreadyExitExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not AlreadyExistException alreadyExistException)
            return false;
        
        var ex = new ProblemDetails
        {
            Detail = alreadyExistException.Message,
            Status = alreadyExistException.StatusCode
        };
        
        httpContext.Response.StatusCode = alreadyExistException.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(ex, cancellationToken);
        return true;
    }
}