using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class OrderDetailManager : IOrderDetailService
{
    IOrderDetailDal _orderDetailDal;
    IProductService _productService;

    public OrderDetailManager(IOrderDetailDal orderDetailDal, IProductService productService)
    {
        _orderDetailDal = orderDetailDal;
        _productService = productService;
    }


    public IResult Add(OrderDetailDto orderDetailDto, int id)
    {
        
        IResult result = BusinessRules.Run(checkIfProductAlreadyInOrder(id, orderDetailDto.ProductId),
            checkIfQuantityIsAvailable(_productService, orderDetailDto.ProductId, orderDetailDto.Quantity));
        if (result != null)
        {
            return result;
            
        }
        
        OrderDetail orderDetail = new OrderDetail
        {
            OrderId = id,
            ProductId = orderDetailDto.ProductId,
            Quantity = orderDetailDto.Quantity,
            UnitPrice = _productService.GetById(orderDetailDto.ProductId).Data.UnitPrice,
        };
        _orderDetailDal.Add(orderDetail);
        return new SuccessResult("Order detail added successfully.");
    }

    public IDataResult<List<OrderDetail>> GetAll()
    {
        var orderDetails = _orderDetailDal.GetAll();
      
        return new SuccessDataResult<List<OrderDetail>>(orderDetails, "Order details retrieved successfully.");
    }

    public IDataResult<List<OrderDetail>> GetAllByOrderId(int orderId)
    {
        var orderDetails = _orderDetailDal.GetAll(od => od.OrderId == orderId);
        return new SuccessDataResult<List<OrderDetail>>(orderDetails, "Order details retrieved successfully.");
    }
    

    public IResult Update(int orderId,OrderDetailDto orderDetailDto)
    {
        var existingOrderDetail = _orderDetailDal.Get(od => od.OrderId == orderId && od.ProductId == orderDetailDto.ProductId);
        if (existingOrderDetail == null)
        {
            return new ErrorResult("Order detail not found.");
        }

        IResult result = BusinessRules.Run(
            checkIfQuantityIsAvailable(_productService, orderDetailDto.ProductId, orderDetailDto.Quantity));
        if (result != null)
        {
            return result;
        }

        existingOrderDetail.Quantity = orderDetailDto.Quantity;
        existingOrderDetail.UnitPrice = _productService.GetById(orderDetailDto.ProductId).Data.UnitPrice;

        _orderDetailDal.Update(existingOrderDetail);
        return new SuccessResult("Order detail updated successfully.");
        
        
    }

    public IResult Delete(int orderId, int productId)
    {
        var existingOrderDetail = _orderDetailDal.Get(od => od.OrderId == orderId && od.ProductId == productId);
        if (existingOrderDetail == null)
        {
            return new ErrorResult("Order detail not found.");
        }

        _orderDetailDal.Delete(existingOrderDetail);
        return new SuccessResult("Order detail deleted successfully.");
    }
    private IResult checkIfQuantityIsAvailable(IProductService productService, int productId, int quantity)
    {
        var productResult = productService.GetById(productId);
        if (productResult.Data == null)
        {
            return new ErrorResult("Product does not exist.");
        }

        if (productResult.Data.UnitsInStock < quantity)
        {
            return new ErrorResult("Insufficient stock (" +productResult.Data.UnitsInStock+ ")for the requested quantity(" +quantity + ").");
        }

        return new SuccessResult();
    }
    

    private IResult checkIfProductAlreadyInOrder(int orderId, int productId)
    {
        var existingOrderDetail = _orderDetailDal.Get(od => od.OrderId == orderId && od.ProductId == productId);
        if (existingOrderDetail != null)
        {
            return new ErrorResult("Product is already added to order. If you want, you can update the quantity.");
        }

        return new SuccessResult();
    }
    
}