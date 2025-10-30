using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Courses.Models;

public class MainPageCourseViewModel
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public int LessonNumber { get; set; }
    public HomeTaskStatus HomeTaskStatus { get; set; }
}
