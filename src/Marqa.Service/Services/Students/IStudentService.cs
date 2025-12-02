using Marqa.Domain.Enums;
using Marqa.Service.Services.Students.Models;
using Microsoft.AspNetCore.Http;
namespace Marqa.Service.Services.Students;

public interface IStudentService
{
    Task CreateAsync(StudentCreateModel model);
    Task UpdateAsync(int id, StudentUpdateModel model);
    Task DeleteAsync(int id);
    Task<StudentViewModel> GetAsync(int id);
    Task<int> GetByPhoneAsync(string phone);
    Task<int> GetStudentParentByPhoneAsync(string phone);
    Task<List<StudentViewModel>> GetAllByCourseIdAsync(int courseId);
    Task<string> UploadProfilePictureAsync(long studentId, IFormFile picture);
    Task<List<StudentViewModel>> GetAllAsync(StudentFilterModel filterModel);
    Task UpdateStudentCourseStatusAsync(int studentId, int courseId, EnrollmentStatus status);

}
