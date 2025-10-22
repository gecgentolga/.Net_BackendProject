using System.Linq.Expressions;
using System.Runtime.InteropServices.JavaScript;
using Core.Entities;


//core katmanı diğer katmanları referans almaz

namespace Core.DataAccess;

// generic constraint
// class : reference type demek
// IEntity olabilir veya IEntity implemente eden bir nesne olabilir
// new() : new'lenebilir olmalı 
public interface IEntityRepository<T> where T:class,IEntity,new()
{
    List<T> GetAll(Expression<Func<T,bool>> filter = null);
    T Get(Expression<Func<T,bool>> filter);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
