using Marqa.Service.Services.Subjects.Models;

namespace Marqa.Service.Services.Subjects;

public interface ISubjectService
{
    Task CreateAsync(SubjectCreateModel model);
    Task UpdateAsync(int id, SubjectUpdateModel model);
    Task AttachAsync(TeacherSubjectCreateModel model);
    Task DetachSubjectAsync(int teacherId, int subjectId);
    Task DeleteAsync(int id);
    Task<SubjectViewModel> GetAsync(int id);
    Task<List<SubjectViewModel>> GetAllAsync(int companyId);
}