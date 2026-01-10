namespace Marqa.Service.Services.Settings.Models;

public class SettingViewModel
{
    public string Key { get; set; }
    public string Value { get; set; }
    public string Category { get; set; }
    public bool IsEncrypted { get; set; }
}