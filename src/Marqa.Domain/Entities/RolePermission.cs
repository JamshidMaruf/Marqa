namespace Marqa.Domain.Entities;

public class RolePermission : Auditable
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
    
    // Navigation Properties
    public Permission Permission { get; set; }
    public EmployeeRole Role { get; set; }
}