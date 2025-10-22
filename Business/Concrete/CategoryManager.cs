using Business.Abstract;
using Business.BusinessAspects;
using Business.ValidationRules.FluentValidation;
using Core.Entities.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class CategoryManager:ICategoryService
{
    ICategoryDal _categoryDal;
    
    public CategoryManager(ICategoryDal categoryDal)
    {
        _categoryDal = categoryDal;
    }
    
    
    public IDataResult<List<Category>> GetAll()
    {
        return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
    }
    
    public IDataResult<Category> GetById(int categoryId)
    {
        return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));
    }

    public IDataResult<string> GetCategoryDescription(int categoryId)
    {
        var category = _categoryDal.Get(c => c.CategoryId == categoryId);
        if (category == null)
        {
            return new ErrorDataResult<string>("Kategori bulunamadı");
        }
        return new SuccessDataResult<string>(category.Description);
    }

    [SecuredOperation("admin")]
    public IResult Delete(int categoryId)
    {
        
        var category = _categoryDal.Get(c => c.CategoryId == categoryId);
        if (category == null)
        {
            return new ErrorResult("Kategori bulunamadı");
        }
        _categoryDal.Delete(category);
        
        return new SuccessResult("Kategori silindi");

    }
    
    [ValidationAspect(typeof(CategoryValidator))]
    public IResult Update(Category category)
    {
        IResult result = CheckIfCategoryNameExists(category.CategoryName);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }
        _categoryDal.Update(category);
        return new SuccessResult("Kategori güncellendi");
    }
    
    [ValidationAspect(typeof(CategoryValidator))]
    public IResult Add(Category category)
    {
        IResult result = CheckIfCategoryNameExists(category.CategoryName);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }
        
        _categoryDal.Add(category);
        return new SuccessResult("Kategori eklendi");
    }

    private IResult CheckIfCategoryNameExists(string categoryName)
    {
        
        var result = _categoryDal.GetAll(c => c.CategoryName == categoryName).Any();
        if (result)
        {
            return new ErrorResult("Kategori ismi zaten mevcut");
        }

        return new SuccessResult();
    }
    
}