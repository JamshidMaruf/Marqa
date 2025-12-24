using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Enrollments.Models;

public class StudentTransferModel
{
    public int StudentId { get; set; }
    public int FromCourseId { get; set; }
    public int ToCourseId { get; set; }
    public EnrollmentStatus Status { get; set; }
    public DateTime DateOfTransfer { get; set; }
    public CoursePaymentType PaymentType { get; set; }
    public decimal Amount { get; set; }
    public string Reason { get; set; }
}
