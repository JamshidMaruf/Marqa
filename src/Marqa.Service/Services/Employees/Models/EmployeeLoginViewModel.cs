using Marqa.Domain.Entities;

namespace Marqa.Service.Services.Employees.Models;

public class EmployeeLoginViewModel
{
    public int Id { get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
    public List<PermissionInfo> Permissions { get; set; }
    public class PermissionInfo
    {
        public string Name { get; set; }
    }
}