using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Courses.Models;

public class AttachModel
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public CoursePaymentType PaymentType { get; set; }
    public decimal Amount { get; set; }
    public DateTime EnrollmentDate { get; set; }
}
