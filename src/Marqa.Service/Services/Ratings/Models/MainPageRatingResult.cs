namespace Marqa.Service.Services.Ratings.Models;

public class MainPageRatingResult
{
    public int StudentId { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public int TotalPoints { get; set; }
    public int Rank { get; set; }
}