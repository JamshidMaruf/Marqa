namespace Marqa.Domain.Entities;

public class EnrollmentFrozen : Auditable
{
    public int EnrollmentId { get; set; }
    public DateTime FrozenStart { get; set; }
    public DateTime? FrozenEnd { get; set; }
    public bool IsInfinite { get; set; }
    public string Reason { get; set; }

    public Enrollment Enrollment { get; set; }
}

