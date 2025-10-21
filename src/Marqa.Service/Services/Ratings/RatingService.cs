using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Services.Ratings.Models;
using Marqa.Service.Services.StudentPointHistories;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Ratings;

public class RatingService(IUnitOfWork unitOfWork,
    IStudentPointHistoryService pointHistoryService) : IRatingService
{
    public async Task<Rating> GetStudentRatingAsync(int studentId)
    {
        var ratings = await GetAllStudentRatingsAsync();
        return ratings.FirstOrDefault(r => r.StudentId == studentId);
    }
    public async Task<IEnumerable<Rating>> GetAllStudentRatingsAsync()
    {
        var students = unitOfWork.Students.SelectAllAsQueryable()
            .Where(s => s.Courses.Any(c => Convert.ToDateTime(c.EndTime) < DateTime.UtcNow));
        var ratings = new List<Rating>();
        foreach (var student in students)
        {
            ratings.Add(new Rating
            {
                CourseId = student.Courses.FirstOrDefault(c => Convert.ToDateTime(c.EndTime) < DateTime.UtcNow).Id,
                Course = await unitOfWork.Courses.SelectAllAsQueryable()
                    .Where(c => c.Id == student.Courses.FirstOrDefault(c => Convert.ToDateTime(c.EndTime) < DateTime.UtcNow).Id)
                    .Select(c => new Rating.CourseInfo
                    {
                        CourseId = c.Id,
                        CourseName = c.Name
                    }).FirstOrDefaultAsync(),
                StudentId = student.Id,
                StudentName = student.FirstName,
                TotalPoints = await pointHistoryService.GetAsync(student.Id),
            });
        }

        var rank = 1;
        foreach (var rating in ratings.OrderByDescending(r => r.TotalPoints))
        {
            rating.Rank = rank;
            rank++;
        }
        return ratings.OrderBy(r => r.Rank);
    }
    public async Task<IEnumerable<Rating>> GetStudentRatingsByCourseAsync(int courseId)
    {
        var students = unitOfWork.Students.SelectAllAsQueryable()
            .Where(s => s.Courses.Any(c => c.Id == courseId));
        var ratings = new List<Rating>();
        foreach (var student in students)
        {
            ratings.Add(new Rating
            {
                CourseId = courseId,
                Course = await unitOfWork.Courses.SelectAllAsQueryable()
                    .Where(c => c.Id == courseId)
                    .Select(c => new Rating.CourseInfo
                    {
                        CourseId = c.Id,
                        CourseName = c.Name
                    }).FirstOrDefaultAsync(),
                StudentId = student.Id,
                StudentName = student.FirstName,
                TotalPoints = await pointHistoryService.GetAsync(student.Id),
            });
        }
        var rank = 1;
        foreach (var rating in ratings.OrderByDescending(r => r.TotalPoints))
        {
            rating.Rank = rank;
            rank++;
        }
        return ratings.OrderBy(r => r.Rank);
    }
}

