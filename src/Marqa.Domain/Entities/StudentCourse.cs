using System.ComponentModel;
using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class StudentCourse : Auditable
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public StudentStatus Status { get; set; }

    // Navigation
    public Student Student { get; set; }
    public Course Course { get; set; }
}

public class Enrollment : Auditable
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public EnrollmentStatus Status { get; set; }
    
    public Student Student { get; set; }
    public Course Course { get; set; }
}

public class EnrollmentCancel : Auditable
{
    public int EnrollmentId { get; set; }
    public DateTime CancelledAt { get; set; }
    public string Reason { get; set; }
}

public class EnrollmentFrozen : Auditable
{
    public int EnrollmentId { get; set; }
    public DateTime FrozenStart { get; set; }
    public DateTime FrozenEnd { get; set; }
    public string Reason { get; set; }
}

public enum EnrollmentStatus
{
    [Description("Foal")] Active,
    [Description("Nofaol")] Inactive,
    [Description("Cancelled")] Cancelled,
    // ...
}