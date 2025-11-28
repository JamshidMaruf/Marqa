using Marqa.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Handlers;

public class ArgumentIsNotValidExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ArgumentIsNotValidException argumentIsNotValidException)
            return false;
        
        var ex = new ProblemDetails
        {
            Status = argumentIsNotValidException.StatusCode, 
            Title = exception.Message,
        };
            
        httpContext.Response.StatusCode = argumentIsNotValidException.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(ex, cancellationToken);
        return true;
    }
}