using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repositories;

public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository
{
    public async Task<PagedList<Product>> GetAll(ProductsParameters productsParameters)
    {
        var products = await GetAll();
        var orderedProducts = products
            .OrderBy(p => p.Id)
            .AsQueryable();
        
        return PagedList<Product>.ToPagedList(orderedProducts, productsParameters.PageNumber, productsParameters.PageSize);
    }

    public async Task<IEnumerable<Product>> GetByCategoryId(int id)
    {
        var products = await GetAll();
        return products.Where(p => p.CategoryId == id);
    }
}