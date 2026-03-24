using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryId(int id);
}