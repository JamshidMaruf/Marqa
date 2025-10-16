using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Banners.Models;

public class BannerViewModel
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string LinkUrl { get; set; }
    public int DisplayOrder { get; set; }
    public IsActive IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}