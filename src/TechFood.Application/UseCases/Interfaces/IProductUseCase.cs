using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models.Customer;
using TechFood.Application.Models.Product;

namespace TechFood.Application.UseCases.Interfaces;

public interface IProductUseCase
{
    Task<IEnumerable<GetProductResult>> GetAllAsync();

    Task<GetProductResult?> GetByIdAsync(Guid id);

    Task<CreateProductResult> CreateAsync(CreateProductRequest request);

    Task<UpdateProductResult?> UpdateAsync(Guid id, UpdateProductRequest request);

    Task<UpdateProductResult?> UpdateOutOfStockAsync(Guid id, bool request);

    Task<bool> DeleteAsync(Guid id);
}
