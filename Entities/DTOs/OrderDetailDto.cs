using Core.Entities;

namespace Entities.DTOs;

public class OrderDetailDto:IDto
{
    public int ProductId { get; set; } 
    public int Quantity { get; set; }
}