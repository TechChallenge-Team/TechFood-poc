using TechFoodClean.Common.Category;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.Interfaces.Gateway
{
    public interface ICategoryGateway
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category entity);
        Task SaveImageAsync(CreateCategoryRequestDTO categoryDTO, string fileName);
        Task DeleteAsync(Category entity);
    }
}
