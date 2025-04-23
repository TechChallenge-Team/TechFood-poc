using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models;

namespace TechFood.Application.UseCases.Interfaces;

public interface IProductUseCase
{
    Task<IEnumerable<ProductResponseDto>> GetAllAsync();

    Task<ProductResponseDto> GetByIdAsync(Guid id);

    Task CreateAsync(ProductRequestDto request);

    Task UpdateAsync(Guid id, ProductRequestDto request);

    Task UpdateOutOfStockAsync(Guid id, bool request);

    Task DeleteAsync(Guid id);
}
