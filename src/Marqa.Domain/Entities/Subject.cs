namespace Marqa.Domain.Entities;

public class Subject : BaseEntity
{
    public string Name { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
