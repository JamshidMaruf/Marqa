namespace Marqa.Service.Services.HomeTask.Models;
public class HomeTaskCreateModel
{
    public int LessonId { get; set; }
    public DateTime Deadline { get; set; }
    public string Description { get; set; }
}