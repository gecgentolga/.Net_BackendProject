using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework;

//NuGet
//EntityFramework
public class EfProductDal : EfEntityRepositoryBase<Product,NorthwindContext>,IProductDal
{
    public List<ProductDetailDto> GetProductDetails(int id)
    {
        using (NorthwindContext context = new NorthwindContext())
        {
            var result = from p in context.Products
                join c in context.Categories
                    on p.CategoryId equals c.CategoryId
                where p.ProductId == id
                select new ProductDetailDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CategoryName = c.CategoryName,
                    UnitsInStock = p.UnitsInStock
                };
            return result.ToList();
        }
    }
    
}
