using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> Get();
    Task<Product?> GetById(int id);
    Task<Product> Create(Product product);
    Task<Product> Update(Product product);
    Task<Product> Delete(int id);
}