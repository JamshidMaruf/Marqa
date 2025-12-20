using Microsoft.AspNetCore.Hosting;

namespace Marqa.Shared.Services;

public class EnvironmentService : IEnvironmentService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public EnvironmentService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public string WebRootPath => _webHostEnvironment.WebRootPath;
}

