using Microsoft.AspNetCore.Http;

namespace Marqa.Service.Services.Banners.Models;
public class BannerUpdateModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
    public string LinkUrl { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
