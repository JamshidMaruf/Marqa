using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Enums;
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
        var students = unitOfWork.Students.SelectAllAsQueryable();
        var ratings = new List<Rating>();
        foreach (var student in students)
        {
            ratings.Add(new Rating
            {
                CourseId = student.Courses.FirstOrDefault().Id,
                Course = await unitOfWork.Courses.SelectAllAsQueryable()
                    .Where(c => c.Id == student.Courses.FirstOrDefault().Id)
                    .Select(c => new Rating.CourseInfo
                    {
                        CourseId = c.Id,
                        CourseName = c.Name
                    }).FirstOrDefaultAsync(),
                StudentId = student.Id,
                StudentFirstName = student.FirstName,
                StudentLastName = student.LastName,
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
            .Where(s => s.Courses.Any());
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
                StudentFirstName = student.FirstName,
                StudentLastName = student.LastName,
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

    public async Task<List<MainPageRatingResult>> GetMainPageRatingResultAsync(int companyId)
    {
        var students = unitOfWork.Students.SelectAllAsQueryable()
            .Where(s => s.CompanyId == companyId);
        var ratings = new List<MainPageRatingResult>();
        foreach (var student in students)
        {
            ratings.Add(new MainPageRatingResult
            {
                StudentId = student.Id,
                StudentFirstName = student.FirstName,
                StudentLastName = student.LastName,
                TotalPoints = pointHistoryService.GetAsync(student.Id).Result,
            });
        }
        var rank = 1;
        foreach (var rating in ratings.OrderByDescending(r => r.TotalPoints))
        {
            if (ratings.Count() == 3)
                break;
            rating.Rank = rank;
            rank++;
        }
        return ratings.OrderBy(r => r.TotalPoints).Take(3).ToList();
    }

    public Task<List<RatingPageRatingResult>> GetRatingPageRatingResultAsync(int companyId, int? courseId = null, Gender? gender = null)
    {
        throw new NotImplementedException();
    }
}

