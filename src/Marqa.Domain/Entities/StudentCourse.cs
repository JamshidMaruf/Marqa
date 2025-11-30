using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class StudentCourse : Auditable
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public StudentStatus Status { get; set; }
    public DateTime EnrolledDate { get; set; }
    public CoursePaymentType PaymentType { get; set; }
    public decimal Amount { get; set; }

    // Navigation
    public Student Student { get; set; }
    public Course Course { get; set; }
    public DateTime EnrolledDate { get; set; }
    public string TransferReason { get; set; }
}
