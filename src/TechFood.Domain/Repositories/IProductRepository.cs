using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Domain.Entities;

namespace TechFood.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product> GetByIdAsync(Guid id);

    Task CreateAsync(Product product);

    Task UpdateAsync();

    Task DeleteAsync(Product product);
}
