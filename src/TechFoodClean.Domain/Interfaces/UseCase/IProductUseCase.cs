using TechFoodClean.Common.DTO.Product;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Domain.Interfaces.UseCase
{
    public interface IProductUseCase
    {
        Task<IEnumerable<Product>> ListAllAsync();

        Task<Product?> GetByIdAsync(Guid id);

        Task<Product> AddAsync(CreateProductRequestDTO category, string fileName);

        Task<Product?> UpdateAsync(Guid id, UpdateProductRequestDTO category, string fileName);

        Task<bool> DeleteAsync(Guid id);
    }
}
