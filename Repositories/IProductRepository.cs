using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<PagedList<Product>> GetAll(ProductsParameters productsParameters);
    Task<IEnumerable<Product>> GetByCategoryId(int id);
    Task<PagedList<Product>> GetFilteredByPrice(ProductsFilterByPrice productsFilterByPrice);
}