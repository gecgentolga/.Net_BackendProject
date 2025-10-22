using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;

public interface IOrderService
{
    public IDataResult<List<Order>> GetAll();
    public IDataResult<OrderDto> GetOrder(int orderId);
    IResult Add(OrderDto orderDto);
    IResult Delete(int orderId);
    IResult Update(OrderDto orderDto, int orderId);
}