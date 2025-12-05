using Marqa.Domain.Enums;
using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Services.Courses;

public interface ICourseService
{
    Task CreateAsync(CourseCreateModel model);
    Task UpdateAsync(int id, CourseUpdateModel model);
    Task DeleteAsync(int id);
    Task<CourseViewModel> GetAsync(int id);
    Task<CourseUpdateViewModel> GetForUpdateAsync(int id);
    Task<List<CourseViewModel>> GetAllAsync(int companyId, string search, int? subjectId);
    Task<List<MainPageCourseViewModel>> GetCoursesByStudentIdAsync(int studentId); // for mobile
    
    /// <summary>
    /// returns all student active courses
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    Task<List<CourseNamesModel>> GetAllStudentCourseNamesAsync(int studentId); // for dashboard panel
    Task<List<MinimalCourseDataModel>> GetUnEnrolledStudentCoursesAsync(int studentId, int companyId); // for dashboard panel  
    Task<List<CoursePageCourseViewModel>> GetCourseNamesByStudentIdAsync(int studentId);  // for mobile
    Task<List<NonFrozenEnrollmentModel>> GetActiveStudentCoursesAsync(int studentId);
    Task<List<FrozenEnrollmentModel>> GetFrozenCoursesAsync(int studentId);
}
