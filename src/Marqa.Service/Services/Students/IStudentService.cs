using Marqa.Domain.Enums;
using Marqa.Service.Services.Students.Models;
using Microsoft.AspNetCore.Http;

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
    Task<int> GetByPhoneAsync(string phone);
    Task<int> GetStudentParentByPhoneAsync(string phone);
    Task<List<StudentViewModel>> GetAllByCourseIdAsync(int courseId);
    Task<string> UploadProfilePictureAsync(long studentId, IFormFile picture);
    Task<List<StudentViewModel>> GetAll(StudentFilterModel filterModel);
    Task UpdateStudentCourseStatusAsync(int studentId, int courseId, StudentStatus status);
}
