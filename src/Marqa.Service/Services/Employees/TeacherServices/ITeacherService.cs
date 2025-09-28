namespace Marqa.Service.Services.Employees.TeacherServices;
public interface ITeacherService
{
    Task CreateAsync(TeacherCreateModel model);
    Task UpdateAsync(TeacherUpdateModel model);
    Task DeleteAsync(int id);
    Task<TeacherViewModel> GetAsync(int id);
    Task<List<TeacherViewModel>> GetAllAsync(int companyId, string search, string subject);
}
public class TeacherService : ITeacherService
{

}
