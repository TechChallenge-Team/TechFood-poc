using TechFoodClean.Application.Presenters;
using TechFoodClean.Common.DTO.Category;

namespace TechFoodClean.Application.Interfaces.Controller
{
    public interface ICategoryController
    {
        Task<IEnumerable<CategoryPresenter>> ListAllAsync();

        Task<CategoryPresenter?> GetByIdAsync(Guid id);

        Task<CategoryPresenter?> AddAsync(CreateCategoryRequestDTO category);

        Task<CategoryPresenter?> UpdateAsync(Guid id, UpdateCategoryRequestDTO category);

        Task<bool> DeleteAsync(Guid id);
    }
}
