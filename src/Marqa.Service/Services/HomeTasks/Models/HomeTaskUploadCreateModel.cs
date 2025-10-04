using Marqa.Domain.Enums;

namespace Marqa.Service.Services.HomeTasks.Models;
public class HomeTaskUploadCreateModel
{
    public int StudentId { get; set; }
    public int HomeTaskId { get; set; }
    public string Info { get; set; }
}