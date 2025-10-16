using Marqa.Service.Services.StudentPointHistories.Models;

namespace Marqa.Service.Services.StudentPointHistories;
public interface IStudentPointHistoryService
{
    Task AddPointAsync(StudentPointAddModel model);
    Task<StudentPointSummModel> GetAsync(int studentId);
    Task<List<StudentPointHistoryViewModel>> GetAllAsync(int studentId);
}