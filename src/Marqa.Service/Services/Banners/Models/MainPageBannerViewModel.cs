using Microsoft.AspNetCore.Http;
namespace Marqa.Service.Services.Banners.Models;


public class MainPageBannerViewModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string LinkUrl { get; set; }
    public int DisplayOrder { get; set; }
}
