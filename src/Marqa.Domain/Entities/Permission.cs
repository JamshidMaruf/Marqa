namespace Marqa.Domain.Entities;

public class Permission : Auditable
{
    public string Name { get; set; }
    public string Module { get; set; }
    public string Action { get; set; }
    public string Description { get; set; }
}