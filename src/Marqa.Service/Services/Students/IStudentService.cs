using Marqa.Service.Services.Students.Models;
using Marqa.Service.Services.Students.Models.DetailModels;

namespace Marqa.Service.Services.Students;

public interface IStudentService
{
    /// <summary>
    /// This method creates student
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task CreateAsync(StudentCreateModel model);
    Task UpdateAsync(int id, StudentUpdateModel model);
    Task DeleteAsync(int id);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<StudentViewModel> GetAsync(int id);
    
    /// <summary>
    /// StudentDetail bilan birga o'chirish (include qilingan)
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotFoundException"></exception>
    Task<StudentViewModel> GetByPhoneAsync(string phone);
    Task<StudentDetailViewModel> GetStudentParentByPhoneAsync(string phone);
    Task<List<StudentViewModel>> GetAllByCourseIdAsync(int courseId);
}
