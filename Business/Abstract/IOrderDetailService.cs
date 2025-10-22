using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;

public interface IOrderDetailService
{
    public IResult Add(OrderDetailDto orderDetailDto, int orderId);
    public IDataResult<List<OrderDetail>> GetAllByOrderId(int orderId);
    public IDataResult<List<OrderDetail>> GetAll();
    public IResult Update(int orderId,OrderDetailDto orderDetailDto);
    public IResult Delete(int orderId, int productId);
    
}