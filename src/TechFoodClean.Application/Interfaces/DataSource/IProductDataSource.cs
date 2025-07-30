using TechFoodClean.Common.DTO;

namespace TechFoodClean.Application.Interfaces.DataSource
{
    public interface IProductDataSource : IDataSource<ProductDTO>
    {
        Task<CategoryDTO?> GetCategoryByIdAsync(Guid categoryId);
    }
}
