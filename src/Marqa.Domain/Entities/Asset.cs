namespace Marqa.Domain.Entities;

public class Asset : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string FileExtension { get; set; }
}