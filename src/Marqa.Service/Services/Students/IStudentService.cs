using Marqa.Service.Services.Students.Models;

namespace Marqa.Service.Services.Students;

public interface IStudentService
{
    Task CreateAsync(StudentCreateModel model);
    Task UpdateAsync(int id, StudentUpdateModel model);
    Task DeleteAsync(int id);
    Task<StudentViewModel> GetAsync(int id);
    Task<List<StudentViewModel>> GetAllByCourseAsync(int courseId);
}
