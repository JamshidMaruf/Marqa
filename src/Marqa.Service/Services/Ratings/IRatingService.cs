﻿using Marqa.Domain.Enums;
using Marqa.Service.Services.Ratings.Models;

namespace Marqa.Service.Services.Ratings;
public interface IRatingService : IScopedService
{
    Task<Rating> GetStudentRatingAsync(int studentId);
    Task<IEnumerable<Rating>> GetAllStudentRatingsAsync();
    Task<IEnumerable<Rating>> GetStudentRatingsByCourseAsync(int courseId);
    Task<List<MainPageRatingResult>> GetMainPageRatingResultAsync(int companyId);
    Task<RatingPageRatingResult> GetRatingPageRatingResultAsync(int companyId,int? courseId = null,Gender? gender = null);
}