using System.Linq.Expressions;

namespace ApiCatalogo.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(Expression<Func<T, bool>> predicate);
    Task<T> Create(T entity);
    T? Update(T entity);
    T Delete(T entity);
}