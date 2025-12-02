using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class StudentPaymentOperation : Auditable
{
    public string PaymentNumber { get; set; } 
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime GivenDate { get; set; }
    public string Description { get; set; }
    public PaymentOperationType PaymentOperationType { get; set; }
    public decimal CoursePrice { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public byte[] RowVersion { get; set; }
    
    // Navigation Properties
    public Course Course { get; set; }
    public Student Student { get; set; }
}