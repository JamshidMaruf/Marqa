﻿using Marqa.Service.Services.HomeTasks.Models;
using Microsoft.AspNetCore.Http;

namespace Marqa.Service.Services.HomeTasks;

public interface IHomeTaskService : IScopedService
{
    Task CreateAsync(HomeTaskCreateModel model);
    Task UploadHomeTaskFileAsync(int homeTaskId, IFormFile file);
    Task UpdateAsync(int id, HomeTaskUpdateModel model);
    Task DeleteAsync(int id);
    Task<List<HomeTaskViewModel>> GetAsync(int lessonId);
    Task StudentHomeTaskCreateAsync(StudentHomeTaskCreateModel model);
    Task UploadStudentHomeTaskFileAsync(int studentHomeTaskId, IFormFile file);
    Task HomeTaskAssessmentAsync(HomeTaskAssessmentModel model);
}
