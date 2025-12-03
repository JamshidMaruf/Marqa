using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[ApiVersion("1")]
// TODO: Implement default api versioning
[Route("api/v1/[controller]")]
//[Authorize]
public class BaseController : ControllerBase
{
    
}