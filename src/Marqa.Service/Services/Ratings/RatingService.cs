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
        var students = unitOfWork.Students
            .SelectAllAsQueryable(includes: "User");

        var ratings = new List<Rating>();
        foreach (var student in students)
        {
            ratings.Add(new Rating
            {
                CourseId = student.Courses.FirstOrDefault().Id,
                Course = await unitOfWork.Courses
                .SelectAllAsQueryable(c => c.Id == student.Courses.FirstOrDefault().Id)
                    .Select(c => new Rating.CourseInfo
                    {
                        CourseId = c.Id,
                        CourseName = c.Name
                    }).FirstOrDefaultAsync(),
                StudentId = student.Id,
                StudentFirstName = student.User.FirstName,
                StudentLastName = student.User.LastName,
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
        var students = unitOfWork.Students
            .SelectAllAsQueryable(s => s.Courses.Any(), includes: "User");

        var ratings = new List<Rating>();

        foreach (var student in students)
        {
            ratings.Add(new Rating
            {
                CourseId = courseId,
                Course = await unitOfWork.Courses
                .SelectAllAsQueryable(c => c.Id == courseId)
                    .Select(c => new Rating.CourseInfo
                    {
                        CourseId = c.Id,
                        CourseName = c.Name
                    }).FirstOrDefaultAsync(),
                StudentId = student.Id,
                StudentFirstName = student.User.FirstName,
                StudentLastName = student.User.LastName,
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
        var students = unitOfWork.Students
            .SelectAllAsQueryable(s => s.User.CompanyId == companyId, includes: "User");

        var ratings = new List<MainPageRatingResult>();
        foreach (var student in students)
        {
            ratings.Add(new MainPageRatingResult
            {
                StudentId = student.Id,
                StudentFirstName = student.User.FirstName,
                StudentLastName = student.User.LastName,
                TotalPoints = await pointHistoryService.GetAsync(student.Id),
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

    public async Task<RatingPageRatingResult> GetRatingPageRatingResultAsync(int companyId, int? courseId = null, Gender? gender = null)
    {
        if (courseId != null && courseId != 0)
        {
            var query = unitOfWork.Enrollments.SelectAllAsQueryable(
                predicate: sc => sc.CourseId == courseId,
                includes: [ "Student", "User" ]);

            if (gender is not null)
            {
                query = query.Where(s => s.Student.Gender == gender);
            }

            var students = new RatingPageRatingResult
            {
                Students = query.Select(s => new RatingPageRatingResult.StudentInfo
                {
                    StudentId = s.Id,
                    StudentFirstName = s.Student.User.FirstName,
                    StudentLastName = s.Student.User.LastName,
                    ImageName = s.Student.Asset.FileName,
                    ImagePath = s.Student.Asset.FilePath,
                    ImageExtension = s.Student.Asset.FileExtension
                })
            };

            students.Students.ToList().ForEach(async student =>
            {
                student.TotalPoints = await pointHistoryService.GetAsync(student.StudentId);
            });

            students.Students = students.Students.OrderByDescending(s => s.TotalPoints);

            int rank = 1;
            foreach (var student in students.Students)
            {
                student.Rank = rank++;
            }

            return students;
        }
        else
        {
            var query = unitOfWork.Students
                .SelectAllAsQueryable(s => !s.IsDeleted)
                .Include(s => s.Courses)
                .ThenInclude(c => c.Course)
                .Include(s => s.User)
                .Where(s => s.User.CompanyId == companyId);

            if (gender is not null)
            {
                query = query.Where(s => s.Gender == gender);
            }

            var students = new RatingPageRatingResult
            {
                Students = await query.Select(s => new RatingPageRatingResult.StudentInfo
                {
                    StudentId = s.Id,
                    StudentFirstName = s.User.FirstName,
                    StudentLastName = s.User.LastName,
                    ImageName = s.Asset.FileName,
                    ImagePath = s.Asset.FilePath,
                    ImageExtension = s.Asset.FileExtension,
                    Courses = s.Courses.Select(c => new RatingPageRatingResult.CourseInfo
                    {
                        Id = c.CourseId,
                        Name = c.Course.Name
                    })
                })
                    .ToListAsync()
            };

            students.Students.ToList().ForEach(async student =>
            {
                student.TotalPoints = await pointHistoryService.GetAsync(student.StudentId);
            });

            students.Students = students.Students.OrderByDescending(s => s.TotalPoints);

            int rank = 1;
            foreach (var student in students.Students)
            {
                student.Rank = rank++;
            }

            return students;
        }
    }
}
