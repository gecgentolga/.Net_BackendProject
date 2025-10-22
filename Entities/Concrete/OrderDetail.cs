using Core.Entities;

namespace Entities.Concrete;

public class OrderDetail:IEntity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    
}

