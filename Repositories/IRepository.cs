using System.Linq.Expressions;

namespace ApiCatalogo.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(Expression<Func<T, bool>> predicate);
    Task<T> Create(T entity);
    Task<T?> Update(T entity);
    Task<T> Delete(T entity);
}