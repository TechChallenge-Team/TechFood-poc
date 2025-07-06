using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Application.Interfaces.Gateway;
using TechFoodClean.Common.Category;
using TechFoodClean.Common.DTO;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.Gateway
{
    public class CategoryGateway : ICategoryGateway
    {

        private readonly ICategoryDataSource _categoryDataSource;
        private readonly IImageDataSource _imageDataSource;
        private readonly IUnitOfWorkDataSource _unitOfWorkDataSource;
        public CategoryGateway(ICategoryDataSource categoryDataSource,
                               IImageDataSource imageDataSource,
                               IUnitOfWorkDataSource unitOfWorkDataSource)
        {
            _categoryDataSource = categoryDataSource;
            _imageDataSource = imageDataSource;
            _unitOfWorkDataSource = unitOfWorkDataSource;
        }

        public async Task AddAsync(Category entity)
        {
            var categoryDTO = new CategoryDTO
            {
                Name = entity.Name,
                ImageFileName = entity.ImageFileName,
                SortOrder = entity.SortOrder,
                IsDeleted = entity.IsDeleted
            };

            await _categoryDataSource.AddAsync(categoryDTO);

            await _unitOfWorkDataSource.CommitAsync();
        }

        public Task DeleteAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            var categoryDTO = await _categoryDataSource.GetByIdAsync(id);

            return categoryDTO is not null ?
                               new Category(categoryDTO.Name,
                                            categoryDTO.ImageFileName,
                                            categoryDTO.SortOrder,
                                            categoryDTO.IsDeleted,
                                            categoryDTO.Id) :
                               null;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categoryDTO = await _categoryDataSource.GetAllAsync();

            return categoryDTO.Select(x =>
                        new Category(x.Name,
                                     x.ImageFileName,
                                     x.SortOrder,
                                     x.IsDeleted,
                                     x.Id)
                        ).ToList();
        }

        public async Task SaveImageAsync(CreateCategoryRequestDTO categoryDTO, string fileName)
        {
            await _imageDataSource.SaveAsync(categoryDTO.File.OpenReadStream(), fileName, nameof(Category));
        }
    }
}
