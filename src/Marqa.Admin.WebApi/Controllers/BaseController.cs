﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Asp.Versioning.ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
#if (!DEBUG)
[Authorize]
#endif
public class BaseController : ControllerBase
{
    
}