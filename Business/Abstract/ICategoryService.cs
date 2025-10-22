using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface ICategoryService
{
    IDataResult<List<Category>> GetAll();

    IDataResult<Category> GetById(int categoryId);
    
    IDataResult<String> GetCategoryDescription(int categoryId);

    IResult Delete(int categoryId);

    IResult Update(Category category );

    //  IResult AddTransactionalTest(Category category);
    IResult Add(Category category);
}