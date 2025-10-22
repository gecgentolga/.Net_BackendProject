using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
  IUserService _userService;
  
  public UserController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpGet("getclaims")]
  public IActionResult GetClaims(int id)
  {
    var result = _userService.GetClaims(id);
    if (result != null)
    {
      return Ok(result);
    }
    return BadRequest("Kullanıcıya ait yetki bulunamadı");
  }
  
  [HttpGet("getbymail")]
  public IActionResult GetByMail(string email)
  {
    var result = _userService.GetByMail(email);
    if (result != null)
    {
      return Ok(result);
    }
    return BadRequest("Kullanıcı bulunamadı");
  }
  
  [HttpPost("add")]
  public IActionResult Add(User user)
  {
    _userService.Add(user);
    return Ok("Kullanıcı eklendi");
  }
}