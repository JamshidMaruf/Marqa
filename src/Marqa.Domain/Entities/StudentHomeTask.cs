using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class StudentHomeTask : Auditable
{
    public int StudentId { get; set; }
    public int HomeTaskId { get; set; }
    public string Info { get; set; }
    public double Score { get; set; } // score uchun alohida result table qilgan yaxshimasmi yoki feedback tablega scoreni qoshib ketsak ham boladi
    public DateTime UploadedAt { get; set; }
    public StudentHomeTaskStatus Status { get; set; } = StudentHomeTaskStatus.none;
    public StudentHomeTaskFile StudentHomeTaskFile { get; set; }
    public StudentHomeTaskFeedback StudentHomeTaskFeedback { get; set; }
}
