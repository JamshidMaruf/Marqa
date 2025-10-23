namespace Marqa.Domain.Entities;

public class Company : Auditable
{
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<Subject> Subjects { get; set; }
}
