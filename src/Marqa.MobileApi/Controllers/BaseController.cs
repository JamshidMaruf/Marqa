using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.MobileApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "StudentApp, TeacherApp, ParentApp")]
public class BaseController : ControllerBase
{

}