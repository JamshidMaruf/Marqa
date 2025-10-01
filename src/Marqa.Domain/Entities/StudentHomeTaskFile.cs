namespace Marqa.Domain.Entities;

public class StudentHomeTaskFile : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public int StudentHomeTaskId { get; set; }
    public StudentHomeTask StudentHomeTask { get; set; }
}
