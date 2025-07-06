using TechFoodClean.Common.Category;
using TechFoodClean.Common.DTO;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.Interfaces.UseCase
{
    public interface ICategoryUseCase
    {
        Task<IEnumerable<Category>> ListAllAsync();

        Task<Category?> GetByIdAsync(Guid id);

        Task<Category> AddAsync(CreateCategoryRequestDTO category, string fileName);

        Task<Category?> UpdateAsync(Guid id, CategoryDTO category);

        Task<bool> DeleteAsync(Guid id);
    }
}
