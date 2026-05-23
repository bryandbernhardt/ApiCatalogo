using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using X.PagedList;
using X.PagedList.Extensions;

namespace ApiCatalogo.Repositories;

public class CategoryRepository(AppDbContext context) : Repository<Category>(context), ICategoryRepository
{
    public async Task<IPagedList<Category>> GetAll(CategoriesParameters categoriesParameters)
    {
        var categories = await GetAll();
        var orderedCategories = categories
            .OrderBy(p => p.Id)
            .AsQueryable();
        
        // return Pagination.PagedList<Category>.ToPagedList(orderedCategories, categoriesParameters.PageNumber, categoriesParameters.PageSize);
        return orderedCategories.ToPagedList(categoriesParameters.PageNumber, categoriesParameters.PageSize);
    }

    public async Task<IPagedList<Category>> GetFilteredByName(CategoriesFilterName categoriesFilterName)
    {
        var categories = await GetAll();

        if (!string.IsNullOrEmpty(categoriesFilterName.Name))
        {
            categories = categories.Where(c => c.Name != null && c.Name.Contains(categoriesFilterName.Name, StringComparison.CurrentCultureIgnoreCase));
        }
        
        // var pagedCategories = Pagination.PagedList<Category>.ToPagedList(
        //     categories.AsQueryable(),
        //     categoriesFilterName.PageNumber,
        //     categoriesFilterName.PageSize);

        var pagedCategories = categories.ToPagedList(categoriesFilterName.PageNumber, categoriesFilterName.PageSize);
        
        return pagedCategories;
    }
}