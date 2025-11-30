namespace Marqa.Domain.Entities;

public class EnrollmentCancellation : Auditable
{
    public int EnrollmentId { get; set; }
    public DateTime CancelledAt { get; set; }
    public string Reason { get; set; }

    public Enrollment Enrollment { get; set; }
}

