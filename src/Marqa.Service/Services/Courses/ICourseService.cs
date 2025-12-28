﻿using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Services.Courses.Models;
using Marqa.Shared.Models;

namespace Marqa.Service.Services.Courses;

public interface ICourseService : IScopedService
{
    Task CreateAsync(CourseCreateModel model);
    Task UpdateAsync(int id, CourseUpdateModel model);
    Task DeleteAsync(int id);
    Task<CourseViewModel> GetAsync(int id);
    Task<CourseUpdateViewModel> GetForUpdateAsync(int id);
    Task MergeAsync(CourseMergeModel model);
    Task<List<CourseTableViewModel>> GetAllAsync(PaginationParams @params,int companyId, string search, CourseStatus? status);
    Task<List<CourseMinimalListModel>> GetMinimalListAsync(int companyId);

    /// <summary>
    /// returns all student active courses
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    Task<List<CourseNamesModel>> GetAllStudentCourseNamesAsync(int studentId); 
    Task<List<CourseMinimalListModel>> GetUnEnrolledStudentCoursesAsync(int studentId, int companyId);
    Task<List<CourseNamesModel>> GetActiveStudentCoursesAsync(int studentId);
    Task<List<FrozenEnrollmentModel>> GetFrozenCoursesAsync(int studentId);
    Task<UpcomingCourseViewModel> GetUpcomingCourseStudentsAsync(int courseId);
    Task<List<MergedCourseViewModel>> GetMergedCoursesAsync(int companyId, PaginationParams @params);
    //Task<List<StudentList>> GetStudentsListAsync(int courseId);
    Task CreateTeacherAssessmentAsync(TeacherAssessment model);
    /// <summary>
    /// This method bulk enrolls students into a course based on the provided model.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task BulkEnrollStudentsAsync(BulkEnrollStudentsModel model);
    Task<CoursesStatistics> GetStatisticsAsync(int companyId);


    #region ForStudentMobile
    Task<List<MainPageCourseViewModel>> GetCoursesByStudentIdAsync(int studentId); 
    Task<List<CoursePageCourseViewModel>> GetCourseNamesByStudentIdAsync(int studentId);
    #endregion
}
