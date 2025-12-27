using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Lessons.Models;

public class LessonViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly Date { get; set; }
    public int Number {  get; set; }
    public HomeTaskStatus HomeTaskStatus { get; set; }
}
