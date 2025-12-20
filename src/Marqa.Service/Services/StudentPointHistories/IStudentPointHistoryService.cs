﻿using Marqa.Service.Services.StudentPointHistories.Models;

namespace Marqa.Service.Services.StudentPointHistories;
public interface IStudentPointHistoryService : IScopedService
{
    Task AddPointAsync(StudentPointAddModel model);
    Task<int> GetAsync(int studentId);
    Task<List<StudentPointHistoryViewModel>> GetAllAsync(int studentId);
}