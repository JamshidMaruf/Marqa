using Microsoft.AspNetCore.Mvc;

namespace Marqa.Mobile.Student.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "StudentApp, TeacherApp, ParentApp")]
public class BaseController : ControllerBase
{

}
