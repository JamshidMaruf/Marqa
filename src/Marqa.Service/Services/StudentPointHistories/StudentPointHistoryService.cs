using System.Data.Common;
using System.Reflection;
using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.StudentPointHistories.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.StudentPointHistories;

public class StudentPointHistoryService(
    IUnitOfWork unitOfWork) : IStudentPointHistoryService
{
    public async Task AddPointAsync(StudentPointAddModel model)
    {
        var student = await unitOfWork.Students.SelectAsync(model.StudentId)
            ?? throw new NotFoundException("This student is not found!");

        var summ = await GetAsync(model.StudentId);

        if (model.Operation == PointHistoryOperation.Minus)
        {
            var createPoint = await unitOfWork.StudentPointHistories
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
            var createPoint = await unitOfWork.StudentPointHistories
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
        var studentPointsHistory = await unitOfWork.StudentPointHistories
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

    public async Task<int> GetAsync(int studentId)
    {
        var studentPointsHistory = await unitOfWork.StudentPointHistories
            .SelectAllAsQueryable()
            .Where(t => t.StudentId == studentId)
            .ToListAsync();

        var minus = studentPointsHistory
            .Where(s => s.Operation == PointHistoryOperation.Minus)
            .Select(s => s.GivenPoint).Sum();

        var plus = studentPointsHistory
            .Where(s => s.Operation == PointHistoryOperation.Plus)
            .Select(s => s.GivenPoint).Sum();

        return plus - minus;
    }
}