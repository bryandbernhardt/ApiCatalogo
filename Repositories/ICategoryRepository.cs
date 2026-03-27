using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<PagedList<Category>> GetAll(CategoriesParameters categoriesParameters);
    Task<PagedList<Category>> GetFilteredByName(CategoriesFilterName categoriesFilterName);
}