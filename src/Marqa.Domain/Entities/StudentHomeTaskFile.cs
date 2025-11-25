namespace Marqa.Domain.Entities;

public class StudentHomeTaskFile : Auditable
{
    public int AssetId { get; set; }
    public int StudentHomeTaskId { get; set; }

    public Asset Asset { get; set; }
    public StudentHomeTask StudentHomeTask { get; set; }
}
