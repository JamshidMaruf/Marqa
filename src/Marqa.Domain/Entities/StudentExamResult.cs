namespace Marqa.Domain.Entities;

public class StudentExamResult : Auditable
{
    public int StudentId { get; set; }
    public int ExamId { get; set; }
    public double Score { get; set; }
    public string TeacherFeedback { get; set; }

    // Navigation
    public Student Student { get; set; }
    public Exam Exam { get; set; }
}
