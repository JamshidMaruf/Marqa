namespace Marqa.Domain.Entities;

public class Company : Auditable
{
    public string Name { get; set; }
    public ICollection<Teacher> Teachers { get; set; }
}
