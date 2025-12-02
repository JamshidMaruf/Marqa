using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Enrollments.Models;

public class EnrollmentCreateModel
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public StudentStatus Status { get; set; }
    public CoursePaymentType PaymentType { get; set; }
    public decimal Amount { get; set; }
}
