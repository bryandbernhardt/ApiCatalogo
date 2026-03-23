using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> Get()
    {
        return await _context
            .Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetById(int id)
    {
        return await _context
            .Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> Create(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product> Update(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        
        return product;
    }

    public async Task<Product> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        ArgumentNullException.ThrowIfNull(product);
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        
        return product;
    }
}