namespace Marqa.Service.Services.Enrollments.Models;

public class FreezeModel
{
    public int StudentId { get; set; }
    public List<int> CourseIds { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsInDefinite { get; set; }
    public string Reason { get; set; }
}
