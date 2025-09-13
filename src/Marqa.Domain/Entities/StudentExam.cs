namespace Marqa.Domain.Entities;

public class StudentExam : BaseEntity
{
    public int StudentId { get; set; }
    public int ExamId { get; set; }
    public double Score { get; set; }

    // Navigation
    public Student Student { get; set; }
    public Exam Exam { get; set; }
}