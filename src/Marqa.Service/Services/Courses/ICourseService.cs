using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Services.Courses;

public interface ICourseService
{
    Task CreateAsync(CourseCreateModel model);
    Task UpdateAsync(int id, CourseUpdateModel model);
    Task DeleteAsync(int id);
    Task<CourseViewModel> GetAsync(int id);
    Task<List<CourseViewModel>> GetAllAsync(int companyId, string search, int? subjectId);
    Task AttachStudentAsync(int courseId, int studentId);
    Task DetachStudentAsync(int courseId, int studentId);
    Task<List<MainPageCourseViewModel>> GetCoursesByStudentIdAsync(int studentId);
}