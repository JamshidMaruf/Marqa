namespace Marqa.Domain.Entities;

public class Exam : Auditable
{
    public string Title { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int CourseId { get; set; }

    // Navigation
    public Course Course { get; set; }
    public ExamSetting ExamSetting { get; set; }
    public ICollection<StudentExamResult> ExamResults { get; set; }
}