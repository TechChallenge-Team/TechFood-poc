using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models;

namespace TechFood.Application.UseCases.Interfaces
{
    public interface ICategoryUseCase
    {
        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();

        Task<CategoryViewModel> GetCategoryByIdAsync(Guid id);

        Task<CategoryViewModel> AddCategoryAsync(CategoryViewModel category);

        Task<CategoryViewModel> UpdateCategoryAsync(Guid id, CategoryViewModel category);

        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
