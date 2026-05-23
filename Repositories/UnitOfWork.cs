using ApiCatalogo.Context;

namespace ApiCatalogo.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProductRepository? _productRepository;
    private ICategoryRepository? _categoryRepository;
    
    public AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IProductRepository ProductRepository
    {
        get
        {
            return _productRepository ??= new ProductRepository(_dbContext);
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoryRepository ??= new CategoryRepository(_dbContext);
        }
    }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}