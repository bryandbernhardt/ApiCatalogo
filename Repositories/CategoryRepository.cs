using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repositories;

public class CategoryRepository(AppDbContext context) : Repository<Category>(context), ICategoryRepository
{
    public async Task<PagedList<Category>> GetAll(CategoriesParameters categoriesParameters)
    {
        var categories = await GetAll();
        var orderedCategories = categories
            .OrderBy(p => p.Id)
            .AsQueryable();
        
        return PagedList<Category>.ToPagedList(orderedCategories, categoriesParameters.PageNumber, categoriesParameters.PageSize);
    }
}