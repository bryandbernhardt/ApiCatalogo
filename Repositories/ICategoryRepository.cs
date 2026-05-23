using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using X.PagedList;

namespace ApiCatalogo.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IPagedList<Category>> GetAll(CategoriesParameters categoriesParameters);
    Task<IPagedList<Category>> GetFilteredByName(CategoriesFilterName categoriesFilterName);
}