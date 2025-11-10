using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class StudentPaymentOperations : Auditable
{
    public int StudentId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string Description { get; set; }
    public PaymentOperationType PaymentOperationType { get; set; }
    
    // Navigation Properties
    public Student Student { get; set; }
}