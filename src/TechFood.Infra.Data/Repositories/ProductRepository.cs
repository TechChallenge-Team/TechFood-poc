using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories;

public class ProductRepository(TechFoodContext dbContext) : IProductRepository
{
    private readonly DbSet<Product> _products = dbContext.Products;

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _products.AsNoTracking().ToListAsync();

    public async Task<Product> GetByIdAsync(Guid id)
        => await _products.Where(x => x.Id == id).FirstAsync();

    public async Task CreateAsync(Product product)
    {
        await _products.AddAsync(product);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync()
        => await dbContext.SaveChangesAsync();

    public async Task DeleteAsync(Product product)
    {
        _products.Remove(product);
        await dbContext.SaveChangesAsync();
    }
}
