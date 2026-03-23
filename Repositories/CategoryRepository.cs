using ApiCatalogo.Context;
using ApiCatalogo.Controllers;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiCatalogo.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;
    
    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> Get()
    {
        return await _context
            .Categories
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetWithProducts()
    {
        return await _context
            .Categories
            .Include(c => c.Products)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Category?> GetById(int id)
    {
        return await _context
            .Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(category => category.Id == id);
    }

    public async Task<Category> Create(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<Category> Update(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        
        return category;
    }

    public async Task<Category> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        ArgumentNullException.ThrowIfNull(category);
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        
        return category;
    }
}