﻿using Marqa.Domain.Enums;
using Marqa.Service.Services.Lessons.Models;
using Microsoft.AspNetCore.Http;

namespace Marqa.Service.Services.Lessons;

public interface ILessonService : IScopedService
{
    Task UpdateAsync(int id, LessonUpdateModel model);
    Task CheckUpAsync(LessonAttendanceModel model);
    Task ModifyAsync(int id, string name,HomeTaskStatus homeTaskStatus);
    Task VideoUploadAsync(int id, IFormFile video);
    Task<List<LessonViewModel>> GetByCourseIdAsync(int courseId);
    Task<List<StudentAttendanceModel>> GetCourseStudentsForCheckUpAsync(int lessonId);
}

