using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Exams.Models;
using Microsoft.EntityFrameworkCore;
namespace Marqa.Service.Services.Exams;
public class ExamService(
    IRepository<Exam> examRepositry,
    IRepository<StudentExamResult> studentExamResultRepository)
    : IExamService
{
    public async Task CreateExamAsync(ExamCreateModel model)
    {
        var existExam =  await examRepositry.SelectAllAsQueryable()
            .FirstOrDefaultAsync(e => ((e.StartTime < model.StartTime && model.StartTime > e.EndTime)
            || (e.StartTime < model.EndTime && model.EndTime > e.EndTime))
            && e.CourseId == model.CourseId);
        if (existExam != null)
            throw new AlreadyExistException("Exam already exist for this period");

        await examRepositry.InsertAsync(new Exam
        {
            CourseId = model.CourseId,
            EndTime = model.EndTime,
            StartTime = model.StartTime,
            Title = model.Title
        });
    }
    public async Task UpdateExamAsync(int examId, ExamUpdateModel model)
    {
        var existExam = examRepositry.SelectAllAsQueryable()
            .FirstOrDefault(e => e.Id == examId)
            ?? throw new NotFoundException("Exam not found");

        var existExamDate = await examRepositry.SelectAllAsQueryable()
            .FirstOrDefaultAsync(e => ((e.StartTime < model.StartTime && model.StartTime > e.EndTime)
            || (e.StartTime < model.EndTime && model.EndTime > e.EndTime))
            && e.CourseId == model.CourseId);
        if (existExamDate != null)
            throw new AlreadyExistException("Exam already exist for this period");

        existExam.CourseId = model.CourseId;
        existExam.EndTime = model.EndTime;
        existExam.StartTime = model.StartTime;
        existExam.Title = model.Title;
        await examRepositry.UpdateAsync(existExam);
    }

    public async Task DeleteExamAsync(int examId)
    {
        var existExam = examRepositry.SelectAllAsQueryable()
            .FirstOrDefault(e => e.Id == examId) 
            ?? throw new NotFoundException("Exam not found");
        await examRepositry.DeleteAsync(existExam);
    }

    public async Task<IEnumerable<ExamViewModel>> GetAllExamsAsync(string search, int? courseid)
    {
        var exams = examRepositry.SelectAllAsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            exams = exams.Where(exams => exams.Title.Contains(search));
        if (courseid.HasValue)
            exams = exams.Where(exams => exams.CourseId == courseid);

        return await exams.Include(e => e.Course)
            .Select(e => new ExamViewModel
            {
                Id = e.Id,
                Title = e.Title,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                CourseId = e.CourseId,
                Course = new ExamViewModel.CourseInfo
                {
                    Id = e.Course.Id,
                    Name = e.Course.Name
                }
            }).ToListAsync();
    }

    public async Task<ExamViewModel> GetExamByIdAsync(int examId)
    {
        return await examRepositry.SelectAllAsQueryable()
            .Include(e => e.Course)
            .Where(e => e.Id == examId)
            .Select(e => new ExamViewModel
            {
                Id = e.Id,
                Title = e.Title,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                CourseId = e.CourseId,
                Course = new ExamViewModel.CourseInfo
                {
                    Id = e.Course.Id,
                    Name = e.Course.Name
                }
            }).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Exam not found");
    }

    public async Task<StudentExamResultView> GetExamResultByStudentIdAsync(int studentid)
    {
        return await studentExamResultRepository.SelectAllAsQueryable()
            .Include(r => r.Student)
            .Include(r => r.Exam)
            .Where(r => r.StudentId == studentid)
            .Select(r => new StudentExamResultView
            {
                Id = r.Id,
                StudentId = r.StudentId,
                ExamId = r.ExamId,
                Score = r.Score,
                TeacherFeedback = r.TeacherFeedback,
                Student = new StudentExamResultView.StudentInfo
                {
                    Id = r.Student.Id,
                    FirstName = r.Student.FirstName,
                    LastName = r.Student.LastName,
                    Email = r.Student.Email
                },
                Exam = new StudentExamResultView.ExamInfo
                {
                    Id = r.Exam.Id,
                    Title = r.Exam.Title,
                    StartTime = r.Exam.StartTime,
                    EndTime = r.Exam.EndTime
                }
            }).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Exam result not found");
    }

    public async Task ScoreExam(StudentExamResultCreate model)
    {
        var existExamResult = await studentExamResultRepository.SelectAllAsQueryable()
            .FirstOrDefaultAsync(r => r.StudentId == model.StudentId && r.ExamId == model.ExamId);
        if (existExamResult != null)
            throw new AlreadyExistException("Exam result already exist for this student and exam");
        await studentExamResultRepository.InsertAsync(new StudentExamResult
        {
            StudentId = model.StudentId,
            ExamId = model.ExamId,
            Score = model.Score,
            TeacherFeedback = model.TeacherFeedback
        });
    }

    public async Task UpdateExamScore(int examresultid, StudentExamResultUpdate model)
    {
        var existExamResult = await studentExamResultRepository.SelectAllAsQueryable()
            .FirstOrDefaultAsync(r => r.Id == examresultid)
            ?? throw new NotFoundException("Exam result not found");

        existExamResult.Score = model.Score;
        existExamResult.TeacherFeedback = model.TeacherFeedback;
        await studentExamResultRepository.UpdateAsync(existExamResult);
    }
}
