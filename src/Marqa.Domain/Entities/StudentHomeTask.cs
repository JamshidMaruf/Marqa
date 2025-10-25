using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class StudentHomeTask : Auditable
{
    public int StudentId { get; set; }
    public int HomeTaskId { get; set; }
    public string Info { get; set; }
    public int Score { get; set; }
    public DateTime UploadedAt { get; set; }
    public StudentHomeTaskStatus Status { get; set; }

    public Student Student { get; set; }
    public StudentHomeTaskFile StudentHomeTaskFile { get; set; }
    public StudentHomeTaskFeedback StudentHomeTaskFeedback { get; set; }
}
