using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models.Category;

namespace TechFood.Application.UseCases.Interfaces
{
    public interface ICategoryUseCase
    {
        Task<IEnumerable<CreateCategoryResponse>> GetCategoriesAsync();

        Task<CreateCategoryResponse> GetCategoryByIdAsync(Guid id);

        Task<CreateCategoryResponse> AddCategoryAsync(CreateCategoryRequest category);

        Task<CreateCategoryResponse> UpdateCategoryAsync(Guid id, CreateCategoryRequest category);

        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
