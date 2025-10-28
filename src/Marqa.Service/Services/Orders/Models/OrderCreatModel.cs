namespace Marqa.Service.Services.Orders.Models;

public class OrderCreateModel
{
    public int StudentId { get; set; }



    public List<OrderItemCreateModel> Items { get; set; }
}
