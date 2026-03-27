using System.Linq.Expressions;
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

    public async Task<PagedList<Product>> GetFilteredByPrice(ProductsFilterByPrice productsFilterByPrice)
    {
        var products = await GetAll();
        var queryableProducts = products.AsQueryable();

        if (productsFilterByPrice.Price.HasValue && !string.IsNullOrEmpty(productsFilterByPrice.CriterionPrice))
        {
            var filters = new Dictionary<string, Expression<Func<Product, bool>>>
            {
                { "greater", p => p.Price > productsFilterByPrice.Price.Value },
                { "smaller", p => p.Price < productsFilterByPrice.Price.Value },
                { "equal",   p => p.Price == productsFilterByPrice.Price.Value }
            };

            if (filters.TryGetValue(productsFilterByPrice.CriterionPrice.ToLower(), out var filter))
            {
                queryableProducts = queryableProducts.Where(filter).OrderBy(p => p.Price);
            }
        }

        return PagedList<Product>.ToPagedList(queryableProducts, productsFilterByPrice.PageNumber, productsFilterByPrice.PageSize);
    }   
}