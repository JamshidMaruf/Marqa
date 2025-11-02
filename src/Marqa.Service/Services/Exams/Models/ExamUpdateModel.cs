namespace Marqa.Service.Services.Exams.Models;

public class ExamUpdateModel
{
    public int CourseId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Title { get; set; }
    public ExamSettingData ExamSetting { get; set; }

    public class ExamSettingData
    {
        public float MinScore { get; set; }
        public float MaxScore { get; set; }
        public bool IsGivenCertificate { get; set; }
        public string CertificateFileName { get; set; }
        public string CertificateFilePath { get; set; }
        public string CertificateFileExtension { get; set; }
        public ICollection<ExamSettingItemData> Items { get; set; }
    }

    public class ExamSettingItemData
    {
        public float Score { get; set; }
        public int GivenPoints { get; set; }
    }
}