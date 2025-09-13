namespace Marqa.Domain.Entities;

public class HomeTask : BaseEntity
{
    public int LessonId { get; set; }
    public DateTime Deadline { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    // Navigation
    public Lesson Lesson { get; set; }
}