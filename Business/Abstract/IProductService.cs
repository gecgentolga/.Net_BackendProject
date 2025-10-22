using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;

public interface IProductService
{
    IDataResult<List<Product>> GetAll();
    IDataResult<List<Product>> GetByCategory(int id);

    IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);

    IDataResult<List<ProductDetailDto>> GetProductDetails(int id);

    IDataResult<Product> GetById(int id);
    IResult Add(Product product);
    IResult Update(Product product);
    
    IResult Delete(int productId);
    IResult AddTransactionalTest(Product product);
}