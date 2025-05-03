using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models.Category;

namespace TechFood.Application.UseCases.Interfaces;

public interface ICategoryUseCase
{
    Task<IEnumerable<GetCategoryResult>> ListAllAsync();

    Task<GetCategoryResult?> GetByIdAsync(Guid id);

    Task<CreateCategoryResult> AddAsync(CreateCategoryRequest category);

    Task<UpdateCategoryResult?> UpdateAsync(Guid id, UpdateCategoryRequest category);

    Task<bool> DeleteAsync(Guid id);
}
