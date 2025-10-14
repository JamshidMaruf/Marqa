namespace Marqa.Service.Services.Exams.Models;

public class StudentExamResultCreate
{
    public int StudentId { get; set; }
    public int ExamId { get; set; }
    public double Score { get; set; }
    public string TeacherFeedback { get; set; }
}