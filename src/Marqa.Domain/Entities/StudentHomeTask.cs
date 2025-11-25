using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class StudentHomeTask : Auditable
{
    public int StudentId { get; set; }
    public int HomeTaskId { get; set; }
    public string Feedback { get; set; }
    public int Score { get; set; }
    public DateTime UploadedAt { get; set; }
    public StudentHomeTaskStatus Status { get; set; }

    public Student Student { get; set; }
    public HomeTask HomeTask { get; set; }
}
