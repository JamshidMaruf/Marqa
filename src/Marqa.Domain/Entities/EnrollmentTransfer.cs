namespace Marqa.Domain.Entities;

public class EnrollmentTransfer : Auditable
{
    public int FromEnrollmentId { get; set; }
    public int ToEnrollmentId { get; set; }
    public DateTime TransferTime { get; set; }
    public string Reason { get; set; }

    // Navigation
    public Enrollment FromEnrollment { get; set; }
    public Enrollment ToEnrollment { get; set; }
}

