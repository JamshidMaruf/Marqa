namespace Marqa.Service.Services.Enrollments.Models;

public class UnFreezeModel
{
    public int StudentId { get; set; }
    public List<int> CourseIds { get; set; }
    public DateTime ActivateDate { get; set; }
}
