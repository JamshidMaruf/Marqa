namespace Marqa.Domain.Entities;

public class EnrollmentFrozen : Auditable
{
    public int EnrollmentId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsInDefinite { get; set; }
    public string Reason { get; set; }
    public bool IsUnFrozen { get; set; }

    public Enrollment Enrollment { get; set; }
}

