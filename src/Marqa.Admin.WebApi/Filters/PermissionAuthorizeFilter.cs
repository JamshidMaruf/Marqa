using Marqa.Service.Services.EmployeeRoles;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Marqa.Admin.WebApi.Filters;

public class PermissionAuthorizeFilter(IEmployeeRoleService employeeRoleService, string permission) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var role = context.HttpContext.User.FindFirst("Role")?.Value;

        if (string.IsNullOrEmpty(role))
        {
            context.Result = CreateJsonResult(StatusCodes.Status401Unauthorized, "Unauthorized");
            return;
        }
        
        var result = await employeeRoleService.HasPermissionAsync(role, permission);

        if (!result)
        {
            context.Result = CreateJsonResult(StatusCodes.Status403Forbidden, "You do not have permission to do that");
            return;
        }
    }
    
    private static JsonResult CreateJsonResult(int code, string message)
    {
        var response = new { code, message };
        return new JsonResult(response)
        {
            StatusCode = code,
            ContentType = "application/json"
        };
    }
}