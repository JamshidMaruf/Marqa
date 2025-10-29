namespace Marqa.Service.Services.Ratings.Models;

public class RatingPageRatingResult
{
    public int StudentId { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public string Picture { get; set; }
    public int TotalPoints { get; set; }
    public int Rank { get; set; }
    public List<CourseInfo> Courses { get; set; }

    public class CourseInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}