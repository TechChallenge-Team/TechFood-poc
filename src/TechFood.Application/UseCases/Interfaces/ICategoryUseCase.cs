using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models.Category;

namespace TechFood.Application.UseCases.Interfaces;

public interface ICategoryUseCase
{
    Task<IEnumerable<CategoryResponse>> ListAllAsync();

    Task<CategoryResponse?> GetByIdAsync(Guid id);

    Task<CategoryResponse> AddAsync(CreateCategoryRequest category);

    Task<CategoryResponse?> UpdateAsync(Guid id, UpdateCategoryRequest category);

    Task<bool> DeleteAsync(Guid id);
}
