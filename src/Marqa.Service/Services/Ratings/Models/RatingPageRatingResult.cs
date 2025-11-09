namespace Marqa.Service.Services.Ratings.Models;

public class RatingPageRatingResult
{
    public IEnumerable<StudentInfo> Students { get; set; }

    public class StudentInfo
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public int TotalPoints { get; set; }
        public int Rank { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string ImageExtension { get; set; }
        public IEnumerable<CourseInfo> Courses { get; set; }
    }
    public class CourseInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}