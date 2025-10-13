using Marqa.Domain.Entities;

namespace Marqa.Service.Services.Exams.Models;

public class StudentExamResultView
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int ExamId { get; set; }
    public double Score { get; set; }
    public string TeacherFeedback { get; set; }

    // Navigation properties
    public StudentInfo Student { get; set; }
    public ExamInfo Exam { get; set; }

    public class StudentInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    public class ExamInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}