namespace Marqa.Service.Services.Ratings.Models;

public class Rating
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public string StudentName { get; set; }
    public int TotalPoints { get; set; }
    public int Rank { get; set; }

    public CourseInfo Course { get; set; }

    public class CourseInfo
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}