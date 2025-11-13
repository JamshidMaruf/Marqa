using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Filters;

public class RequirePermissionAttribute : TypeFilterAttribute
{
    public RequirePermissionAttribute(string permission) : base(typeof(PermissionAuthorizeFilter))
    {
        Arguments = new object[] { permission };
    }
}