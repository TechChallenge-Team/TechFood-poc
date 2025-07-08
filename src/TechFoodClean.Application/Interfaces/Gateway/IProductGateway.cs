using Microsoft.AspNetCore.Http;
using TechFoodClean.Common.DTO;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.Interfaces.Gateway
{
    public interface IProductGateway
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product entity);
        Task SaveImageAsync(IFormFile file, string fileName);
        Task DeleteImageAsync(Product category);
        Task UpdateAsync(Product category);
        Task DeleteAsync(Product category);
        Task<CategoryDTO?> GetCategoryByIdAsync(Guid categoryId);
    }
}
