using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Exams.Models;
using Microsoft.EntityFrameworkCore;
namespace Marqa.Service.Services.Exams;
public class ExamService(IUnitOfWork unitOfWork) : IExamService
{
    public async Task CreateExamAsync(ExamCreateModel model)
    {
        var existExam =  await unitOfWork.Exams.SelectAllAsQueryable()
            .FirstOrDefaultAsync(e => ((e.StartTime < model.StartTime && model.StartTime > e.EndTime)
            || (e.StartTime < model.EndTime && model.EndTime > e.EndTime))
            && e.CourseId == model.CourseId);
        if (existExam != null)
            throw new AlreadyExistException("Exam already exist for this period");

        unitOfWork.Exams.Insert(new Exam
        {
            CourseId = model.CourseId,
            EndTime = model.EndTime,
            StartTime = model.StartTime,
            Title = model.Title
        });

        await unitOfWork.SaveAsync();
    }
    public async Task UpdateExamAsync(int examId, ExamUpdateModel model)
    {
        var existExam = unitOfWork.Exams.SelectAllAsQueryable()
            .FirstOrDefault(e => e.Id == examId)
            ?? throw new NotFoundException("Exam not found");

        var existExamDate = await unitOfWork.Exams.SelectAllAsQueryable()
            .FirstOrDefaultAsync(e => ((e.StartTime < model.StartTime && model.StartTime > e.EndTime)
            || (e.StartTime < model.EndTime && model.EndTime > e.EndTime))
            && e.CourseId == model.CourseId);
        
        if (existExamDate != null)
            throw new AlreadyExistException("Exam already exist for this period");

        existExam.CourseId = model.CourseId;
        existExam.EndTime = model.EndTime;
        existExam.StartTime = model.StartTime;
        existExam.Title = model.Title;
        
        unitOfWork.Exams.Update(existExam);

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteExamAsync(int examId)
    {
        var existExam = unitOfWork.Exams.SelectAllAsQueryable()
            .FirstOrDefault(e => e.Id == examId) 
            ?? throw new NotFoundException("Exam not found");
       
        unitOfWork.Exams.Delete(existExam);
        
        await unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<ExamViewModel>> GetAllExamsAsync(string search, int? courseid)
    {
        var exams =unitOfWork.Exams.SelectAllAsQueryable();
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
        return await unitOfWork.Exams.SelectAllAsQueryable()
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

    public async Task<List<StudentExamResultView>> GetExamResultsByStudentIdAsync(int studentid)
    {
        return await unitOfWork.StudentExamResults.SelectAllAsQueryable()
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
            }).ToListAsync();
    }

    public async Task ScoreExam(StudentExamResultCreate model)
    {
        var existExamResult = await unitOfWork.StudentExamResults.SelectAllAsQueryable()
            .FirstOrDefaultAsync(r => r.StudentId == model.StudentId && r.ExamId == model.ExamId);
        
        if (existExamResult != null)
            throw new AlreadyExistException("Exam result already exist for this student and exam");
        
        unitOfWork.StudentExamResults.Insert(new StudentExamResult
        {
            StudentId = model.StudentId,
            ExamId = model.ExamId,
            Score = model.Score,
            TeacherFeedback = model.TeacherFeedback
        });

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateExamScore(int examresultid, StudentExamResultUpdate model)
    {
        var existExamResult = await unitOfWork.StudentExamResults.SelectAllAsQueryable()
            .FirstOrDefaultAsync(r => r.Id == examresultid)
            ?? throw new NotFoundException("Exam result not found");

        existExamResult.Score = model.Score;
        existExamResult.TeacherFeedback = model.TeacherFeedback;
        
        unitOfWork.StudentExamResults.Update(existExamResult);

        await unitOfWork.SaveAsync();
    }
}
