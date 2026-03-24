using ApiCatalogo.Context;
using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories;

public class CategoryRepository(AppDbContext context) : Repository<Category>(context), ICategoryRepository { }