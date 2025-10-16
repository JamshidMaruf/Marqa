namespace Marqa.Domain.Entities;

public class Banner:Auditable
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public string LinkUrl { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}