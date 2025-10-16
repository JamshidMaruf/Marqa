using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marqa.Service.Services.Ratings.Models;

namespace Marqa.Service.Services.Ratings;
public interface IRatingService
{
    Task<Rating> GetStudentRatingAsync(int studentId);
    Task<IEnumerable<Rating>> GetAllStudentRatingsAsync();
    Task<IEnumerable<Rating>> GetStudentRatingsByCourseAsync(int courseId);
}

