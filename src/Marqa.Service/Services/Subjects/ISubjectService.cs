using Marqa.Service.Services.Subjects.Models;

namespace Marqa.Service.Services.Subjects;

public interface ISubjectService
{
    Task CreateAsync(SubjectCreateModel model);
    Task UpdateAsync(int id, SubjectUpdateModel model);
    Task AttachAsync(int teacherId, int subjectId);
    Task DetachAsync(int teacherId, int subjectId);
    Task DeleteAsync(int id);
    Task<SubjectViewModel> GetAsync(int id);
    Task<List<SubjectViewModel>> GetAllAsync(int companyId);
    Task BulkAttachAsync(int teacherId, List<int> subjectIds);
}