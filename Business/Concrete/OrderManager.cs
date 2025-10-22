using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class OrderManager: IOrderService
{
    IOrderDal _orderDal;
    IOrderDetailService _orderDetailService;
    
    public OrderManager(IOrderDal orderDal, IOrderDetailService orderDetailService)
    {
        _orderDal = orderDal;
        _orderDetailService = orderDetailService;
    }


    public IDataResult<List<Order>> GetAll()
    {
        return new SuccessDataResult<List<Order>>(_orderDal.GetAll(), "Orders retrieved successfully.");
    }


    public IDataResult<OrderDto> GetOrder(int orderId)
    {
        var orderDto = _orderDal.GetOrder(orderId);
        return new SuccessDataResult<OrderDto>(orderDto, "Order retrieved successfully.");
        
    }
    
    public IResult Add(OrderDto orderDto)
    {   
       Order order= new Order
           {
            CustomerId = orderDto.CustomerId,
            ShipCity = orderDto.ShipCity
        };
        _orderDal.Add(order);
        foreach (var orderDetailDto in orderDto.OrderDetails)
        {
            _orderDetailService.Add(orderDetailDto, order.OrderId);
        }
        return new SuccessResult("Order added successfully.With orderId: " + order.OrderId);
    }

    //check et if siliniyorsa orderdetails da silinsin
    public IResult Delete(int orderId)
    {
        var order = _orderDal.Get(o => o.OrderId == orderId);
        if (order == null)
        {
            return new ErrorResult("Order not found.");
        }
        _orderDal.Delete(order);
        return new SuccessResult("Order deleted successfully.");
    }

   


    public IResult Update(OrderDto orderDto, int orderId)
    {
        var existingOrder = _orderDal.Get(o => o.OrderId == orderId);
        if (existingOrder == null)
        {
            return new ErrorResult("Order not found.");
        }

        existingOrder.CustomerId = orderDto.CustomerId;
        existingOrder.ShipCity = orderDto.ShipCity;
        _orderDal.Update(existingOrder);

        foreach (var orderDetailDto in orderDto.OrderDetails)
        {
            _orderDetailService.Update(existingOrder.OrderId, orderDetailDto);
        }

        return new SuccessResult("Order updated successfully.");
    }
}