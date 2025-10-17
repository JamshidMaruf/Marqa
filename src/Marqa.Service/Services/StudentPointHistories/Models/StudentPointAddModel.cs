using Marqa.Domain.Enums;

namespace Marqa.Service.Services.StudentPointHistories.Models;
public class StudentPointAddModel
{
    public int StudentId { get; set; }
    public string Note { get; set; }
    public int Point {  get; set; }
    public PointHistoryOperation Operation { get; set; }
}
