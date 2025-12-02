namespace Marqa.Service.Services.Courses.Models;

public class TransferStudentAcrossComaniesModel
{
    public int StudentId { get; set; }
    public int FromCourseId { get; set; }
    public int ToCourseId { get; set; }
    public DateTime DateOfTransfer { get; set; }
    public string Reason { get; set; }
}
