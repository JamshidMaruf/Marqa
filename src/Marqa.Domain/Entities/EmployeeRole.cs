namespace Marqa.Domain.Entities;

public class EmployeeRole : Auditable
{
    public string Name { get; set; }
    public bool CanTeach { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}