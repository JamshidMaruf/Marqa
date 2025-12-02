namespace Marqa.Service.Services.Enrollments.Models;

public class FreezeModel
{
    public int StudentId { get; set; }
    public List<int> CourseIds { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool IsInDefinite { get; set; }
    public string Reason { get; set; }
}
