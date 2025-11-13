using Marqa.Service.Services.EmployeeRoles;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Marqa.Admin.WebApi.Filters;

public class PermissionAuthorizeFilter(IEmployeeRoleService employeeRoleService, string permission) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var role = context.HttpContext?.User?.FindFirst("Role")?.Value;

        if (string.IsNullOrEmpty(role))
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.HttpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Message = "Unauthorized"
            });
            
            return;
        }
        
        var result = await employeeRoleService.HasPermissionAsync(role, permission);

        if (!result)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.HttpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Message = "You do not have permission to do that"
            });
            
            return;
        }
    }
}