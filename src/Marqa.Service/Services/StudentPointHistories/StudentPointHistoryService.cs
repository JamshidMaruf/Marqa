using System.Reflection;
using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.StudentPointHistories.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.StudentPointHistories;

public class StudentPointHistoryService(
    IRepository<StudentPointHistory> studentPointHistoryRepository,
    IRepository<Student> studentRepository) : IStudentPointHistoryService
{
    public async Task AddPointAsync(StudentPointAddModel model)
    {
        var student = await studentRepository.SelectAsync(model.StudentId)
            ?? throw new NotFoundException("This student is not found!");

        var summ = await GetAsync(model.StudentId);

        if (model.Operation == PointHistoryOperation.Minus)
        {
            var createPoint = await studentPointHistoryRepository
                .InsertAsync(new StudentPointHistory
                {
                    StudentId = model.StudentId,
                    GivenPoint = model.Point,
                    Note = model.Note,
                    Operation = model.Operation,
                    PreviousPoint = summ,
                    CurrentPoint = summ - model.Point,
                });
        }
        else if (model.Operation == PointHistoryOperation.Plus)
        {
            var createPoint = await studentPointHistoryRepository
                .InsertAsync(new StudentPointHistory
                {
                    StudentId = model.StudentId,
                    GivenPoint = model.Point,
                    Note = model.Note,
                    Operation = model.Operation,
                    PreviousPoint = summ,
                    CurrentPoint = summ + model.Point,
                });
        }
    }

    public async Task<List<StudentPointHistoryViewModel>> GetAllAsync(int studentId)
    {
        var studentPointsHistory = await studentPointHistoryRepository
           .SelectAllAsQueryable()
           .Where(t => t.StudentId == studentId)
           .Select(s => new StudentPointHistoryViewModel
           {
               Id = s.Id,
               StudentId = studentId,
               Note = s.Note,
               PreviousPoint = s.PreviousPoint,
               CurrentPoint = s.CurrentPoint,
               GivenPoint = s.GivenPoint,
               Operation = s.Operation,
           })
           .ToListAsync();

        return studentPointsHistory;
    }

    public async Task<StudentPointSummModel> GetAsync(int studentId)
    {
        var studentPointsHistory = await studentPointHistoryRepository
            .SelectAllAsQueryable()
            .Where(t => t.StudentId == studentId)
            .ToListAsync();

        var minus = studentPointsHistory
            .Where(s => s.Operation == PointHistoryOperation.Minus)
            .Select(s => s.GivenPoint).Sum();

        var plus = studentPointsHistory
            .Where(s => s.Operation == PointHistoryOperation.Plus)
            .Select(s => s.GivenPoint).Sum();

        return new StudentPointSummModel
        {
            Point = plus - minus,
        };
    }
}