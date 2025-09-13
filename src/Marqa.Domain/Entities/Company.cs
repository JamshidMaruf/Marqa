namespace Marqa.Domain.Entities;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Teacher> Teachers { get; set; }
}
