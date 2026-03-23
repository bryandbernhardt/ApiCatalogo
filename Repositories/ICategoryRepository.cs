using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiCatalogo.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> Get();
    Task<IEnumerable<Category>> GetWithProducts();
    Task<Category?> GetById(int id);
    Task<Category> Create(Category category);
    Task<Category> Update(Category category);
    Task<Category> Delete(int id);
}