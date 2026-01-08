
namespace Marqa.Service.Services.Permissions.Models;

public class PermissionUpdateFormModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Module { get; set; }
    public string Action { get; set; }
    public string Description { get; set; }
}
