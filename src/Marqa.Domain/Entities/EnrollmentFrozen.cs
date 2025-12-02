namespace Marqa.Domain.Entities;

public class EnrollmentFrozen : Auditable
{
    public int EnrollmentId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool IsInDefinite { get; set; }
    public string Reason { get; set; }

    public Enrollment Enrollment { get; set; }
}

