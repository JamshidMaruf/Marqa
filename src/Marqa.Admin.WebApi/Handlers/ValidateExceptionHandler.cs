using Marqa.Service.Exceptions;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace Marqa.Admin.WebApi.Handlers;

public class ValidateExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidateException validateException)
            return false;

        var ex = new ErrorResponse
        {
            StatusCode = validateException.StatusCode,
            Errors = validateException.Errors
        };

        httpContext.Response.StatusCode = validateException.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(ex, cancellationToken);
        return true;
    }
}
