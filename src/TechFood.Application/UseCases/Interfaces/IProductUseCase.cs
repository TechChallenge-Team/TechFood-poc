using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models;

namespace TechFood.Application.UseCases.Interfaces;

public interface IProductUseCase
{
    Task<IEnumerable<ProductResponseDto>> GetProductsAsync();

    Task<ProductResponseDto> GetProductByIdAsync(Guid id);

    Task CreateProductAsync(ProductRequestDto request);

    Task UpdateProductAsync(Guid id, ProductRequestDto request);

    Task DeleteProductAsync(Guid id);
}
