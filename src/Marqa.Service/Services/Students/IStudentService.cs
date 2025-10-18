using Marqa.Service.Services.Students.Models;

namespace Marqa.Service.Services.Students;

public interface IStudentService
{
    Task CreateAsync(StudentCreateModel model);
    Task UpdateAsync(int id, StudentUpdateModel model);
    Task DeleteAsync(int id);
    Task<StudentViewModel> GetAsync(int id);
    Task<List<StudentViewModel>> GetAllByCourseIdAsync(int courseId);
    /*Task SendOTPAsync(string phone);
    Task<(int StudentId, bool IsVerified)> VerifyOTPAsync(string phone, int otpCode);
    Task<List<Account>> GetAccountsAsync(int studentId);*/
}
