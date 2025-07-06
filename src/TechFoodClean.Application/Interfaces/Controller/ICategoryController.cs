using TechFoodClean.Application.Presenters;
using TechFoodClean.Common.Category;
using TechFoodClean.Common.DTO;

namespace TechFoodClean.Application.Interfaces.Controller
{
    public interface ICategoryController
    {
        Task<IEnumerable<CategoryPresenter>> ListAllAsync();

        Task<CategoryPresenter?> GetByIdAsync(Guid id);

        Task<CategoryPresenter?> AddAsync(CreateCategoryRequestDTO category);

        Task<CategoryPresenter?> UpdateAsync(Guid id, CategoryDTO category);

        Task<bool> DeleteAsync(Guid id);
    }
}
