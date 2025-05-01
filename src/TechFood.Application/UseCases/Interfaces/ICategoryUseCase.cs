using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models.Category;

namespace TechFood.Application.UseCases.Interfaces
{
    public interface ICategoryUseCase
    {
        Task<IEnumerable<CategoryResponse>> GetCategoriesAsync();

        Task<CategoryResponse> GetCategoryByIdAsync(Guid id);

        Task<CategoryResponse> AddCategoryAsync(CreateCategoryRequest category);

        Task<CategoryResponse> UpdateCategoryAsync(Guid id, UpdateCategoryRequest category);

        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
