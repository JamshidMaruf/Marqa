using Marqa.Domain.Enums;

namespace Marqa.Service.Services.StudentPointHistories.Models;
public class StudentPointHistoryViewModel
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int PreviousPoint { get; set; }
    public int GivenPoint { get; set; }
    public int CurrentPoint { get; set; }
    public DateTime GivenDateTime { get; set; }
    public string Note { get; set; }
    public PointHistoryOperation Operation { get; set; }
}
