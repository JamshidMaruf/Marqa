using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Enrollments.Models;

public class EnrollmentStatusViewModel
{
    public List<EnrollmentStatusData> Statuses { get; set; }

    public class EnrollmentStatusData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}