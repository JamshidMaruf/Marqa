namespace Marqa.Domain.Entities;

public class Subject : Auditable
{
    public string Name { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
