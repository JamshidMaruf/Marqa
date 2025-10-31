namespace Marqa.Domain.Entities;

public class Exam : Auditable
{
    public int CourseId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Title { get; set; }

    // Navigation
    public Course Course { get; set; }
    public ICollection<StudentExamResult> ExamResults { get; set; }
}