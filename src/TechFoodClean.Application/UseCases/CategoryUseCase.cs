using TechFoodClean.Application.Interfaces.Gateway;
using TechFoodClean.Application.Interfaces.UseCase;
using TechFoodClean.Common.Category;
using TechFoodClean.Common.DTO;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.UseCases
{
    public class CategoryUseCase : ICategoryUseCase
    {
        private readonly ICategoryGateway _categoryGateway;
        public CategoryUseCase(ICategoryGateway categoryGateway)
        {
            _categoryGateway = categoryGateway;
        }

        public async Task<Category> AddAsync(CreateCategoryRequestDTO categoryDTO, string fileName)
        {
            var categoryEntity = new Category(categoryDTO.Name, fileName, 0);

            await _categoryGateway.SaveImageAsync(categoryDTO, fileName);

            await _categoryGateway.AddAsync(categoryEntity);

            return categoryEntity;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Category?> GetByIdAsync(Guid id)
        {
            return _categoryGateway.GetByIdAsync(id);
        }

        public Task<IEnumerable<Category>> ListAllAsync()
        {
            return _categoryGateway.GetAllAsync();
        }

        public Task<Category?> UpdateAsync(Guid id, CategoryDTO category)
        {
            throw new NotImplementedException();
        }
    }
}
