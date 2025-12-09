namespace Marqa.Service.Services.Enums.Models;

public class YearlyMonths
{
    public int CurrentYear { get; set; }
    public List<MonthData> Months { get; set; }

    public class MonthData
    {
        public byte Id { get; set; }
        public string Name { get; set; }
    }
}