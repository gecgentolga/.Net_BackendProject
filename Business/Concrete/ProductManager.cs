using Business.Abstract;
using Business.BusinessAspects;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Validation;
using Core.Entities.Aspects.Autofac.Caching;
using Core.Entities.Aspects.Autofac.Validation;
using Core.Transaction;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    IProductDal _productDal;
    ICategoryService _categoryService;

    public ProductManager(IProductDal productDal, ICategoryService categoryService)
    {
        _productDal = productDal;
        _categoryService = categoryService;
    }

    [CacheAspect] //key,value
    public IDataResult<List<Product>> GetAll()
    {
        //İŞ KODLARI
        //Yetkisi var mı?
        if (DateTime.Now.Hour == 89)
        {
            return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
        }

        //product.Id ye göre sıralama yapalım
        return new SuccessDataResult<List<Product>>(
            _productDal.GetAll().OrderBy(p => p.ProductId).ToList(), 
            Messages.ProductsListed);
    }
 
    public IDataResult<List<Product>> GetByCategory(int id)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id),
            Messages.ProductsListed);
    }

    public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max),
            Messages.ProductsListed);
    }

   

    public IDataResult<List<ProductDetailDto>> GetProductDetails(int id)
    {
        return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(id),Messages.ProductDetailsListed);
    }

    [CacheAspect]
    [PerformanceAspect(5)] //5 saniyeyi geçerse uyar
    public IDataResult<Product> GetById(int id)
    {
        if((_productDal.Get(p => p.ProductId == id)) == null)
        {
            return new ErrorDataResult<Product>(Messages.ProductNotFound);
        }
        return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id));
    }

    [SecuredOperation("product.add,admin")]
    [ValidationAspect(typeof(ProductValidator))]
    [CacheRemoveAspect("IProductService.Get")] 
    public IResult Add(Product product)
    {
        IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
            CheckIfProductCountOfCategoryCorrect(product.ProductId),
            CheckIfCategoryLimitExceded());
        if (result != null)
        {
            return result;
        }

        _productDal.Add(product);
        return new SuccessResult(Messages.ProductAdded);
    }

    [ValidationAspect(typeof(ProductValidator))]
    [CacheRemoveAspect("IProductService.Get")]
    public IResult Update(Product product)
    {
        _productDal.Update(product);
        return new SuccessResult(Messages.ProductUpdated);
    }

    [SecuredOperation("product.delete,admin")]
    public IResult Delete(int productId)
    {
        var product = _productDal.Get(p => p.ProductId == productId);
        if (product == null)
        {
            return new ErrorResult(Messages.ProductNotFound);
        }

        _productDal.Delete(product);
        return new SuccessResult("Product deleted");
    }

    [TransactionScopeAspect]
    public IResult AddTransactionalTest(Product product)
    {
        throw new NotImplementedException();
    }

    private IResult CheckIfProductCountOfCategoryCorrect(int CategoryId)
    {
        var result = _productDal.GetAll(p => p.CategoryId == CategoryId).Count;
        if (result > 10)
        {
            return new ErrorResult(Messages.ProductCountOfCategoryError);
        }

        return new SuccessResult();
    }

    private IResult CheckIfProductNameExists(string productName)
    {
        var result = _productDal.GetAll(p => p.ProductName == productName).Any();
        if (result)
        {
            return new ErrorResult(Messages.ProductNameAlreadyExists);
        }

        return new SuccessResult();
    }

    private IResult CheckIfCategoryLimitExceded()
    {
        var countCategory = _categoryService.GetAll().Data.Count;
        if (countCategory > 15)
        {
            return new ErrorResult(Messages.CategoryLimitExceded);
        }

        return new SuccessResult();
    }
}