using TechFoodClean.Common.DTO.Category;
using TechFoodClean.Domain.Entities;
using TechFoodClean.Domain.Interfaces.Gateway;
using TechFoodClean.Domain.Interfaces.UseCase;

namespace TechFoodClean.Domain.UseCases
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

            await _categoryGateway.SaveImageAsync(categoryDTO.File, fileName);

            await _categoryGateway.AddAsync(categoryEntity);

            return categoryEntity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await _categoryGateway.GetByIdAsync(id);

            if (category != null)
            {
                await _categoryGateway.DeleteAsync(category);

                await _categoryGateway.DeleteImageAsync(category);

                return true;
            }

            return false;
        }

        public Task<Category?> GetByIdAsync(Guid id)
        {
            return _categoryGateway.GetByIdAsync(id);
        }

        public Task<IEnumerable<Category>> ListAllAsync()
        {
            return _categoryGateway.GetAllAsync();
        }

        public async Task<Category?> UpdateAsync(Guid id, UpdateCategoryRequestDTO categoryDTO, string fileName)
        {
            var category = await _categoryGateway.GetByIdAsync(id);

            if (category == null)
            {
                return null;
            }

            var imageFileName = string.IsNullOrEmpty(fileName) ? category.ImageFileName : fileName;

            if (categoryDTO.File != null)
            {
                await _categoryGateway.SaveImageAsync(categoryDTO.File, imageFileName);

                await _categoryGateway.DeleteImageAsync(category);
            }

            category.UpdateAsync(categoryDTO.Name, imageFileName);

            await _categoryGateway.UpdateAsync(category);

            return category;
        }
    }
}
