using Marqa.Domain.Entities;

namespace Marqa.Service.Services.Exams.Models;

public class ExamViewModel
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Title { get; set; }

    public CourseInfo Course { get; set; }

    public class CourseInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}