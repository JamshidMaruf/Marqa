﻿using Marqa.Domain.Enums;
using Marqa.Service.Services.Teachers.Models;
using Marqa.Shared.Models;

namespace Marqa.Service.Services.Teachers;

public interface ITeacherService : IScopedService
{
    Task CreateAsync(TeacherCreateModel model);
    Task UpdateAsync(int id, TeacherUpdateModel model);
    Task DeleteAsync(int id);
    Task<TeacherViewModel> GetAsync(int id);
    Task<TeacherUpdateViewModel> GetForUpdateAsync(int id);
    Task<List<TeacherTableViewModel>> GetAllAsync(int companyId, PaginationParams @params, string search = null, TeacherStatus? status = null);
    Task<CalculatedTeacherSalaryModel> CalculateTeacherSalaryAsync(int teacherId, int year, Month month);
    List<TeacherPaymentGetModel> GetTeacherPaymentTypes();
    Task<TeacherStatistics> GetTeacherStatisticsAsync(int teacherId);
    Task<TeachersStatistics> GetStatisticsAsync(int companyId);
    Task<List<TeacherMinimalListModel>> GetMinimalListAsync(int companyId);

}
