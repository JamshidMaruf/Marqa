namespace Marqa.Domain.Entities;

public class Lesson : Auditable
{
    public int Number { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateOnly Date { get; set; }
    public string Room { get; set; }
    public int CourseId { get; set; }
    
    // shuni qoshdim ozimdan nega deganda lessonni boshqa oqituvchi otib beradigan bolsa log qilib ketish uchun 
    // bundan tashqari lessonga beriladigan summani oqituvchiga transfer qilish uchun ham kerak boladi
    public int TeacherId { get; set; }
    // Navigation
    public Course Course { get; set; }
    public Employee Teacher { get; set; }
    public ICollection<LessonVideo> Videos { get; set; }
    public ICollection<LessonFile> Files { get; set; }
}
