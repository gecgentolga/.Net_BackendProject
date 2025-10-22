using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Loosely coupled-gevşek bağlılık
        //naming convention-- isimlendirme kuralı
        //SOLID
        //IoC Container -- Inversion of Control
        IProductService _productService;
        
        
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(int productId)
        {
            var result = _productService.Delete(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

       
        
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
  
        }

        [HttpGet("getbycategory")]
        public IActionResult GetByCategory(int id)
        {
            var result = _productService.GetByCategory(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyunitprice")]
        public IActionResult GetByUnitPrice(decimal min, decimal max)
        {
            var result = _productService.GetByUnitPrice(min, max);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getproductdetails")]
        public IActionResult GetProductDetails(int id)
        {
            var result = _productService.GetProductDetails(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        
        

    }
}