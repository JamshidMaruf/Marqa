using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace Marqa.Service.Services.Courses.Models;

public class FrozenEnrollmentModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Level { get; set; }
    public DateTime FrozenDate {  get; set; }
    public DateTime? EndDate { get; set; }
    public string Reason { get; set; }
}