namespace Marqa.Domain.Entities;

public class StudentHomeTaskResult : Auditable
{
    public int StudentId { get; set; }
    public int HomeTaskId { get; set; }
    public double Score { get; set; }

    // Navigation
    public Student Student { get; set; }
    public HomeTask HomeTask { get; set; }
}
