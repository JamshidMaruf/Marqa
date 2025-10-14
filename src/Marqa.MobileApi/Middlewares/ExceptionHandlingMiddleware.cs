using Marqa.Service.Exceptions;
using Marqa.MobileApi.Models;

namespace Marqa.MobileApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch(NotFoundException ex)
        {
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch(AlreadyExistException ex)
        {
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch(ArgumentIsNotValidException ex)
        {
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch(Exception ex)
        {
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = 500,
                Message = ex.Message
            });
        }
    }
}