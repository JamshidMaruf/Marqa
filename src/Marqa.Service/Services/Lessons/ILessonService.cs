using Marqa.Domain.Enums;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Services.Lessons;

public interface ILessonService
{
    Task UpdateAsync(int id, LessonUpdateModel model);
    Task CheckUpAsync(LessonAttendanceModel model);
    Task ModifyAsync(int id, string name, HomeTaskStatus homeTaskStatus);
    Task<List<LessonViewModel>> GetAllByCourseIdAsync(int courseId);
}

