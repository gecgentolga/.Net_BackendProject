using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : Controller
{
    ICategoryService _categoryService;


    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _categoryService.GetAll();
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    
    [HttpGet("getbyid")]
    public IActionResult GetById(int categoryId)
    {
        var result = _categoryService.GetById(categoryId);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpGet("getdescription")]
    public IActionResult GetCategoryDescription(int categoryId)
    {
        var result = _categoryService.GetCategoryDescription(categoryId);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result);
    }

    [HttpPost("add")]
    public IActionResult Add(Category category)
    {
        var result = _categoryService.Add(category);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);

    }

    [HttpPost("update")]
    public IActionResult Update(Category category)
    {
        var result = _categoryService.Update(category);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPost("delete")]
    public IActionResult Delete(int categoryId)
    {
        var result = _categoryService.Delete(categoryId);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
            
    }
    
}