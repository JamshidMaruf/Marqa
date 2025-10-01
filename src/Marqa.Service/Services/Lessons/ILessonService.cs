using Marqa.Domain.Enums;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Services.Lessons;

public interface ILessonService
{
    Task UpdateAsync(int id, LessonUpdateModel model);
    Task CheckUpAsync(LessonAttendanceModel model);
}

