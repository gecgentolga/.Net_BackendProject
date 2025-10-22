using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework;

public class EfOrderDal:EfEntityRepositoryBase<Order,NorthwindContext>,IOrderDal
{

    public OrderDto GetOrder(int orderId)
    {
        using (NorthwindContext context = new NorthwindContext())
        {
            var result = from o in context.Orders
                where o.OrderId == orderId
                select new OrderDto
                {
                    CustomerId = o.CustomerId,
                    ShipCity = o.ShipCity,
                    OrderDetails = (from od in context.OrderDetails
                                    where od.OrderId == o.OrderId
                                    select new OrderDetailDto
                                    {
                                        ProductId = od.ProductId,
                                        Quantity = od.Quantity
                                    }).ToList()
                };
          return result.FirstOrDefault();
        }
    }
}