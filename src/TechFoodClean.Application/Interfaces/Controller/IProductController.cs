using TechFoodClean.Application.Presenters;
using TechFoodClean.Common.DTO.Product;

namespace TechFoodClean.Application.Interfaces.Controller
{
    public interface IProductController
    {
        Task<IEnumerable<ProductPresenter>> ListAllAsync();

        Task<ProductPresenter?> GetByIdAsync(Guid id);

        Task<ProductPresenter?> AddAsync(CreateProductRequestDTO product);

        Task<ProductPresenter?> UpdateAsync(Guid id, UpdateProductRequestDTO product);

        Task<bool> DeleteAsync(Guid id);
    }
}
