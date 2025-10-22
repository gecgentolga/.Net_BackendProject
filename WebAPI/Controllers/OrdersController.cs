using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : Controller
{
    IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _orderService.GetAll();
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpGet("getorder")]
    public IActionResult GetOrder(int orderId)
    {
        var result = _orderService.GetOrder(orderId);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] OrderDto orderDto)
    {
        
        var result = _orderService.Add(orderDto);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPost("delete")]
    public IActionResult Delete(int orderId)
    {
        var result = _orderService.Delete(orderId);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPost("update")]
    public IActionResult Update([FromBody] OrderDto orderDto, int orderId)
    {
        
        var result = _orderService.Update(orderDto, orderId);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
}