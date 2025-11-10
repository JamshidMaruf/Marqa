using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class EmployeeSalary : Auditable
{
    public int EmployeeId { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateTime { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    
    // Navigation
    public Employee Employee { get; set; }
}