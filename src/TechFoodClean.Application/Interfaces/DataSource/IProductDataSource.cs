using TechFoodClean.Common.DTO;
using TechFoodClean.Common.Entities;

namespace TechFoodClean.Application.Interfaces.DataSource
{
    public interface IProductDataSource : IDataSource<ProductDTO>
    {
        Task<CategoryDTO?> GetCategoryByIdAsync(Guid categoryId);
    }
}
