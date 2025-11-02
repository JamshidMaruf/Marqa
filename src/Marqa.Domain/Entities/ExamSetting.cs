namespace Marqa.Domain.Entities;

public class    ExamSetting : Auditable
{
    public int ExamId { get; set; }
    public float MinScore { get; set; }
    public float MaxScore { get; set; }
    public bool IsGivenCertificate { get; set; }
    public string CertificateFileName { get; set; }
    public string CertificateFilePath { get; set; }
    public string CertificateFileExtension { get; set; }
    
    public Exam Exam { get; set; }
    public ICollection<ExamSettingItem> Items { get; set; }
}