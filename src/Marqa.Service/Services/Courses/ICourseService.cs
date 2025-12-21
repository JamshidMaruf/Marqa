﻿using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Services.Courses;

public interface ICourseService : IScopedService
{
    Task CreateAsync(CourseCreateModel model);
    Task UpdateAsync(int id, CourseUpdateModel model);
    Task DeleteAsync(int id);
    Task<CourseViewModel> GetAsync(int id);
    Task<CourseUpdateViewModel> GetForUpdateAsync(int id);
    Task<List<CourseTableViewModel>> GetAllAsync(int companyId, string search, CourseStatus? status);
    Task<List<CourseMinimalListModel>> GetMinimalListAsync(int companyId);
    Task<List<MainPageCourseViewModel>> GetCoursesByStudentIdAsync(int studentId); // for mobile
    
    /// <summary>
    /// returns all student active courses
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    Task<List<CourseNamesModel>> GetAllStudentCourseNamesAsync(int studentId); // for dashboard panel
    Task<List<CourseMinimalListModel>> GetUnEnrolledStudentCoursesAsync(int studentId, int companyId); // for dashboard panel  
    Task<List<CoursePageCourseViewModel>> GetCourseNamesByStudentIdAsync(int studentId);  // for mobile
    Task<List<CourseNamesModel>> GetActiveStudentCoursesAsync(int studentId);
    Task<List<FrozenEnrollmentModel>> GetFrozenCoursesAsync(int studentId);
    Task<UpcomingCourseViewModel> GetUpcomingCourseStudentsAsync(int courseId);
    Task CreateTeacherAssessmentAsync(TeacherAssessment model);

    /// <summary>
    /// This method bulk enrolls students into a course based on the provided model.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task BulkEnrollStudentsAsync(BulkEnrollStudentsModel model);
}
