using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class EmployeePayment : Auditable
{
    public int EmployeeId { get; set; }
    public string PaymentNumber { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public EmployeePaymentOperationType EmployeePaymentOperationType { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string Description { get; set; }

    //Navigation
    public Employee Employee { get; set; } 
}