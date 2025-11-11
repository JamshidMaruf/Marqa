using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Exams.Models;
using Microsoft.EntityFrameworkCore;
namespace Marqa.Service.Services.Exams;

public class ExamService(IUnitOfWork unitOfWork,
    IValidator<ExamCreateModel> examCreateValidator,
    IValidator<ExamUpdateModel> examUpdateValidator,
    IValidator<StudentExamResultCreate> studentExamCreateValidator,
    IValidator<StudentExamResultUpdate> studentExamUpdateValidator) : IExamService
{
    public async Task CreateExamAsync(ExamCreateModel model)
    {
        await examCreateValidator.EnsureValidatedAsync(model);

        var existExam = await unitOfWork.Exams
            .SelectAllAsQueryable(e =>
            e.CourseId == model.CourseId &&
            (e.EndTime > model.StartTime && e.StartTime < model.EndTime))
            .FirstOrDefaultAsync();

        if (existExam != null)
            throw new AlreadyExistException($"Exam already exist for this course between this time frame");

        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var createdExam = unitOfWork.Exams.Insert(new Exam
            {
                CourseId = model.CourseId,
                EndTime = model.EndTime,
                StartTime = model.StartTime,
                Title = model.Title
            });

            await unitOfWork.SaveAsync();

            var createdExamSetting = unitOfWork.ExamSettings.Insert(new ExamSetting
            {
                ExamId = createdExam.Id,
                MinScore = model.ExamSetting.MinScore,
                MaxScore = model.ExamSetting.MaxScore,
                IsGivenCertificate = model.ExamSetting.IsGivenCertificate,
                CertificateFileName = model.ExamSetting.CertificateFileName,
                CertificateFilePath = model.ExamSetting.CertificateFilePath,
                CertificateFileExtension = model.ExamSetting.CertificateFileExtension
            });

            await unitOfWork.SaveAsync();

            await unitOfWork.ExamSettingItems.InsertRangeAsync(model.ExamSetting.Items.Select(item => new ExamSettingItem
            {
                ExamSettingId = createdExamSetting.Id,
                Score = item.Score,
                GivenPoints = item.GivenPoints
            }));

            await unitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateExamAsync(int examId, ExamUpdateModel model)
    {
        await examUpdateValidator.EnsureValidatedAsync(model);

        var examForUpdation = await unitOfWork.Exams
            .SelectAllAsQueryable(e => e.IsDeleted)
            .Include(e => e.ExamSetting)
            .ThenInclude(e => e.Items)
            .FirstOrDefaultAsync(e => e.Id == examId)
                ?? throw new NotFoundException("Exam not found");

        var existExamDate = await unitOfWork.Exams
            .SelectAllAsQueryable(e =>
            e.CourseId == model.CourseId &&
            (e.EndTime > model.StartTime && e.StartTime < model.EndTime))
            .FirstOrDefaultAsync();

        if (existExamDate != null && existExamDate.Id != examId)
            throw new AlreadyExistException($"Exam already exist for this course between this time frame");

        unitOfWork.ExamSettingItems.RemoveRange(examForUpdation.ExamSetting.Items);

        examForUpdation.CourseId = model.CourseId;
        examForUpdation.EndTime = model.EndTime;
        examForUpdation.StartTime = model.StartTime;
        examForUpdation.Title = model.Title;
        examForUpdation.ExamSetting.MinScore = model.ExamSetting.MinScore;
        examForUpdation.ExamSetting.MaxScore = model.ExamSetting.MaxScore;
        examForUpdation.ExamSetting.IsGivenCertificate = model.ExamSetting.IsGivenCertificate;
        examForUpdation.ExamSetting.CertificateFileName = model.ExamSetting.CertificateFileName;
        examForUpdation.ExamSetting.CertificateFilePath = model.ExamSetting.CertificateFilePath;
        examForUpdation.ExamSetting.CertificateFileExtension = model.ExamSetting.CertificateFileExtension;

        await unitOfWork.ExamSettingItems.InsertRangeAsync(model.ExamSetting.Items.Select(item => new ExamSettingItem
        {
            ExamSettingId = examForUpdation.ExamSetting.Id,
            Score = item.Score,
            GivenPoints = item.GivenPoints
        }));

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteExamAsync(int examId)
    {
        var exam = await unitOfWork.Exams
            .SelectAllAsQueryable(e => !e.IsDeleted,
            new[] { "e => e.ExamResults" })
            .Include(e => e.ExamSetting)
            .ThenInclude(es => es.Items)
            .FirstOrDefaultAsync(e => e.Id == examId)
            ?? throw new NotFoundException("Exam not found");

        if (exam.ExamSetting != null)
        {
            foreach (var item in exam.ExamSetting.Items)
                unitOfWork.ExamSettingItems.MarkAsDeleted(item);

            unitOfWork.ExamSettings.MarkAsDeleted(exam.ExamSetting);
        }

        foreach (var item in exam.ExamResults)
            unitOfWork.StudentExamResults.MarkAsDeleted(item);

        unitOfWork.Exams.MarkAsDeleted(exam);

        await unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<ExamViewModel>> GetAllExamsAsync(string search, int? courseid)
    {
        var exams = unitOfWork.Exams.SelectAllAsQueryable(e => !e.IsDeleted);

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
        return await unitOfWork.Exams.SelectAllAsQueryable(e => !e.IsDeleted,
        new[] { "e => e.Course" })
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
        return await unitOfWork.StudentExamResults
            .SelectAllAsQueryable(e => !e.IsDeleted,
            new[] { "r => r.Student", "r => r.Exam" })
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
        await studentExamCreateValidator.EnsureValidatedAsync(model);

        var existExamResult = await unitOfWork.StudentExamResults
            .SelectAllAsQueryable(r => r.StudentId == model.StudentId && r.ExamId == model.ExamId)
            .FirstOrDefaultAsync();

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
        await studentExamUpdateValidator.EnsureValidatedAsync(model);

        var existExamResult = await unitOfWork.StudentExamResults
            .SelectAllAsQueryable(r => r.Id == examresultid)
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException("Exam result not found");

        existExamResult.Score = model.Score;
        existExamResult.TeacherFeedback = model.TeacherFeedback;

        unitOfWork.StudentExamResults.Update(existExamResult);

        await unitOfWork.SaveAsync();
    }
}
