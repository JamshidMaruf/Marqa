namespace Marqa.Domain.Entities;

public class Setting : Auditable
{
    public string Key { get; set; }
    public string Value { get; set; }
    public string Category { get; set; }
    public bool IsEncrypted { get; set; }
}