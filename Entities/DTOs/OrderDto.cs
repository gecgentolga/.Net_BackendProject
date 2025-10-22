using Core.Entities;

namespace Entities.DTOs;

public class OrderDto:IDto
{
    public string CustomerId { get; set; }
    public string ShipCity { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; }
    
}