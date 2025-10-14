namespace Marqa.Service.Services.Exams.Models;

public class ExamCreateModel
{
    public int CourseId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Title { get; set; }
}