using Marqa.Service.Services.Teachers.Models;

namespace Marqa.Service.Services.Teachers;

public interface ITeacherService
{
    Task CreateAsync(TeacherCreateModel model);
    Task UpdateAsync(int id, TeacherUpdateModel model);
    Task DeleteAsync(int id);
    Task<TeacherViewModel> GetAsync(int id);
    Task<List<TeacherViewModel>> GetAllAsync(int companyId, string search = null, int? subjectId = null);
}
