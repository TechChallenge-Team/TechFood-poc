using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models.Product;

namespace TechFood.Application.UseCases.Interfaces;

public interface IProductUseCase
{
    Task<IEnumerable<GetProductResult>> GetAllAsync();

    Task<GetProductResult> GetByIdAsync(Guid id);

    Task CreateAsync(CreateProductRequest request);

    Task UpdateAsync(Guid id, UpdateProductRequest request);

    Task UpdateOutOfStockAsync(Guid id, bool request);

    Task DeleteAsync(Guid id);
}
