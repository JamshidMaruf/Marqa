namespace Marqa.Service.Services.Courses.Models;

public class CourseMergeModel
{
    public int FromCourseId { get; set; }
    public int ToCourseId { get; set; }
    public string Reason{ get; set; }
    public DateTime DateofMerge { get; set; }
}