namespace Marqa.Service.Services.Enrollments.Models;

public class DetachModel
{
    public int StudentId { get; set; }
    public List<int> CourseIds { get; set; }
    public string Reason { get; set; }
    public DateTime DeactivatedDate { get; set; }
}