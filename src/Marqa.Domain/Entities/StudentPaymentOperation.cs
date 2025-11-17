using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class StudentPaymentOperation : Auditable
{
    public string PaymentNumber { get; set; } // unique identifier of the payment for user
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string Description { get; set; }
    public PaymentOperationType PaymentOperationType { get; set; }
    public decimal CoursePrice { get; set; }
    public byte[] RowVersion { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    
    // Navigation Properties
    public Course Course { get; set; }
    public Student Student { get; set; }
}