namespace Marqa.Service.Services.EmployeeRoles.Models;

public class EmployeeRoleCreateModel
{
    public string Name { get; set; }
    public bool CanTeach { get; set; }
    public int CompanyId { get; set; }
}