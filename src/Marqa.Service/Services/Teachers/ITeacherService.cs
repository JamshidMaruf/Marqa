﻿using Marqa.Domain.Enums;
using Marqa.Service.Services.Teachers.Models;

namespace Marqa.Service.Services.Teachers;

public interface ITeacherService : IScopedService
{
    Task CreateAsync(TeacherCreateModel model);
    Task UpdateAsync(int id, TeacherUpdateModel model);
    Task DeleteAsync(int id);
    Task<TeacherViewModel> GetAsync(int id);
    Task<TeacherUpdateViewModel> GetForUpdateAsync(int id);
    Task<List<TeacherTableViewModel>> GetAllAsync(int companyId, string search = null, int? subjectId = null);
    Task<CalculatedTeacherSalaryModel> CalculateTeacherSalaryAsync(int teacherId, int year, Month month);
    Task<List<TeacherPaymentGetModel>> GetTeacherPaymentTypes();
    Task<TeacherStatistics> GetTeacherStatisticsAsync(int teacherId);
}
