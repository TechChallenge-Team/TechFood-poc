using Microsoft.EntityFrameworkCore;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Common.Entities;
using TechFoodClean.Infrastructure.Data.Contexts;

namespace TechFoodClean.Infrastructure.Data.Repositories;

public class ProductRepository(TechFoodContext dbContext) : IProductDataSource
{
    private readonly DbSet<ProductDTO> _products = dbContext.Products;

    public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        => await _products.AsNoTracking().ToListAsync();

    public async Task<ProductDTO?> GetByIdAsync(Guid id)
        => await _products.Where(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<Guid> AddAsync(ProductDTO product)
    {
        var session = await _products.AddAsync(product);

        return session.Entity.Id;
    }

    public async Task DeleteAsync(ProductDTO product)
        => await Task.FromResult(_products.Remove(product));
}
