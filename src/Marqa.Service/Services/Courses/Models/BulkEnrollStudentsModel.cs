namespace Marqa.Service.Services.Courses.Models;

public class BulkEnrollStudentsModel
{
    public int CourseId { get; set; }
    public List<int> StudentIds { get; set; }
    public DateTime EnrollmentDate { get; set; }
}