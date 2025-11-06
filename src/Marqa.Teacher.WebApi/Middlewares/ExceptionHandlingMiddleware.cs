using Marqa.Service.Exceptions;
using Marqa.Teacher.WebApi.Models;

namespace Marqa.Teacher.WebApi.Middlewares;

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
        catch(NotFoundException ex)
        {
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch(AlreadyExistException ex)
        {
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch(ArgumentIsNotValidException ex)
        {
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch(NotMatchedException ex)
        {
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch(Exception ex)
        {
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                Status = 500,
                Message = ex.Message
            });
            logger.LogError(ex, ex.Message);
        }
    }
}