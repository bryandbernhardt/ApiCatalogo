using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using X.PagedList;

namespace ApiCatalogo.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IPagedList<Product>> GetAll(ProductsParameters productsParameters);
    Task<IEnumerable<Product>> GetByCategoryId(int id);
    Task<IPagedList<Product>> GetFilteredByPrice(ProductsFilterByPrice productsFilterByPrice);
}