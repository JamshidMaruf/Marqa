using Marqa.DataAccess.Contexts;
using Marqa.Service.Services.Courses;
using Marqa.Service.Services.Courses.Models;
using Microsoft.EntityFrameworkCore;


var service = new CourseService();

await service.CreateAsync(new CourseCreateModel
{
    Name = "Test14",
    LessonCount = 12,
    StartDate = new DateOnly(2025, 09, 10),
    SubjectId = 2,
    TeacherId = 2,
    Room = "120",
    StartTime = new TimeSpan(16, 00, 00),
    EndTime = new TimeSpan(18, 00, 00),
    Weekdays = new List<DayOfWeek>
    {
        DayOfWeek.Monday,
        DayOfWeek.Wednesday,
        DayOfWeek.Friday,
    }
});