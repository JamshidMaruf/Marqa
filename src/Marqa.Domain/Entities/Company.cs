namespace Marqa.Domain.Entities;

public class Company : Auditable
{
    public string Name { get; set; }
    public ICollection<Employee> Teachers { get; set; }
}
