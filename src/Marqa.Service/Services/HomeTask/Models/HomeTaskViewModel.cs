namespace Marqa.Service.Services.HomeTask.Models;
public class HomeTaskViewModel
{
    public int Id { get; set; }
    public int LessonId { get; set; }
    public DateTime Deadline { get; set; }
    public string Description { get; set; }
}
