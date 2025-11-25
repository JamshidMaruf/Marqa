namespace Marqa.Domain.Entities;

public class HomeTaskFile : Auditable
{
    public int HomeTaskId { get; set; }
    public int AssetId { get; set; }

    public HomeTask HomeTask { get; set; }
    public Asset Asset { get; set; }
}
