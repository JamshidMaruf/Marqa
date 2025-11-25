using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class EmployeeSalary : Auditable
{
    public string PaymentNumber { get; set; } // this is unique indentifier of the payment for user not for system
    public int EmployeeId { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateTime { get; set; }
    public string? Description { get; set; } 
    public PaymentMethod PaymentMethod { get; set; }
    public byte[] RowVersion { get; set; }
    // Navigation
    public Employee Employee { get; set; }
}