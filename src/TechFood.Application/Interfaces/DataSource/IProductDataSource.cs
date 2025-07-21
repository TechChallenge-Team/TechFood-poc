using TechFood.Common.DTO;
using TechFood.Common.Entities;

namespace TechFood.Application.Interfaces.DataSource
{
    public interface IProductDataSource : IDataSource<ProductDTO>
    {
        Task<CategoryDTO?> GetCategoryByIdAsync(Guid categoryId);
    }
}
