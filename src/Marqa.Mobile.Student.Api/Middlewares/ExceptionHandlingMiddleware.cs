using Marqa.Service.Exceptions;
using Marqa.Shared.Models;

namespace Marqa.Mobile.Student.Api.Middlewares;


public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlingMiddleware> logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (NotFoundException ex)
        {
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (AlreadyExistException ex)
        {
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (ArgumentIsNotValidException ex)
        {
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = 500,
                Message = "Server error occured",
            });

            logger.LogError(ex, ex.Message);
        }
    }
}
