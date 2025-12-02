namespace Marqa.Service.Services.Courses.Models;

public class FrozenEnrollmentModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Level { get; set; }
    public DateOnly FrozenDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string Reason { get; set; }
}
