using Microsoft.AspNetCore.Http;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.Interfaces.Gateway
{
    public interface ICategoryGateway
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category entity);
        Task SaveImageAsync(IFormFile file, string fileName);
        Task DeleteImageAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
