using Marqa.Service.Services.Exams.Models;

namespace Marqa.Service.Services.Exams;
public interface IExamService
{
    Task CreateExamAsync(ExamCreateModel model);
    Task UpdateExamAsync(int examId, ExamUpdateModel model);
    Task<ExamViewModel> GetExamByIdAsync(int examId);
    Task<IEnumerable<ExamViewModel>> GetAllExamsAsync(string? search, int? courseid);
    Task DeleteExamAsync(int examId);

    Task ScoreExam(StudentExamResultCreate model);
    Task UpdateExamScore(int examresultid, StudentExamResultUpdate model);
    Task<StudentExamResultView> GetExamResultByStudentIdAsync(int studentid);
}
