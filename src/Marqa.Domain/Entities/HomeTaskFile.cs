namespace Marqa.Domain.Entities;

public class HomeTaskFile : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string FileExtension { get; set; }
    public int HomeTaskId { get; set; }
    public HomeTask HomeTask { get; set; }
}
