using Marqa.Domain.Enums;

namespace Marqa.Service.Services.HomeTasks.Models;

public class HomeTaskAssessmentModel
{
    public int StudentHomeTaskId { get; set; }
    public int TeacherId { get; set; }
    public int Score { get; set; }
    public string FeedBack { get; set; }
    public StudentHomeTaskStatus Status { get; set; }
}
