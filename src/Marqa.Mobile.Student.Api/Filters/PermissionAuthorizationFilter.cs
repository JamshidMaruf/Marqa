using Microsoft.AspNetCore.Mvc.Filters;

namespace Marqa.Mobile.Student.Api.Filters;

public class PermissionAuthorizationFilter : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var role = context.HttpContext.User.FindFirst("EntityType");
        
        Console.WriteLine($"Role: {role.Value}");
        
        throw new NotImplementedException();
    }
}