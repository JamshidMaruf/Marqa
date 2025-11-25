namespace Marqa.Domain.Entities;

public class Company : Auditable
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Director { get; set; }
    public ICollection<Employee> Employees { get; set; }
    public ICollection<EmployeeRole> Roles { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<Subject> Subjects { get; set; }
}