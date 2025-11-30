using System.ComponentModel;
using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class Enrollment : Auditable
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public StudentStatus StudentStatus { get; set; }
    public DateTime EnrolledDate { get; set; }
    public CoursePaymentType PaymentType { get; set; }
    public decimal Amount { get; set; }
    public EnrollmentStatus Status { get; set; }

    // Navigation
    public Student Student { get; set; }
    public Course Course { get; set; }
    public EnrollmentCancellation EnrollmentCancellation { get; set; }
}


public class EnrollmentCancellation : Auditable
{
    public int EnrollmentId { get; set; }
    public DateTime CancelledAt { get; set; }
    public string Reason { get; set; }

    public Enrollment Enrollment { get; set; }
}

public class EnrollmentFrozen : Auditable
{
    public int EnrollmentId { get; set; }
    public DateTime FrozenStart { get; set; }
    public DateTime? FrozenEnd { get; set; }
    public bool IsInfinite { get; set; }
    public string Reason { get; set; }

    public Enrollment Enrollment { get; set; }
}

