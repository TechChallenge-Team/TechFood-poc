using TechFoodClean.Application.Gateway;
using TechFoodClean.Application.Interfaces.Controller;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Application.Interfaces.Gateway;
using TechFoodClean.Application.Interfaces.Presenter;
using TechFoodClean.Application.Interfaces.UseCase;
using TechFoodClean.Application.Presenters;
using TechFoodClean.Application.UseCases;
using TechFoodClean.Common.Category;
using TechFoodClean.Common.DTO;

namespace TechFoodClean.Application.Controllers
{
    public class CategoryController : ICategoryController
    {
        private readonly ICategoryGateway _categoryGateway;
        private readonly ICategoryUseCase _categoryUseCase;
        private readonly IImageUrlResolver _imageUrlResolver;
        public CategoryController(ICategoryDataSource categoryDataSource,
                                  IImageUrlResolver imageUrlResolver,
                                  IImageDataSource imageDataSource,
                                  IUnitOfWorkDataSource unitOfWorkDataSource)
        {
            _categoryGateway = new CategoryGateway(categoryDataSource, imageDataSource, unitOfWorkDataSource);
            _categoryUseCase = new CategoryUseCase(_categoryGateway);
            _imageUrlResolver = imageUrlResolver;
        }

        public async Task<CategoryPresenter?> AddAsync(CreateCategoryRequestDTO category)
        {
            var imageFileName = _imageUrlResolver.CreateImageFileName(category.Name, category.File.ContentType);

            var result = await _categoryUseCase.AddAsync(category, imageFileName);

            return result is not null ?
                   CategoryPresenter.Create(result, _imageUrlResolver) :
                   null;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryPresenter?> GetByIdAsync(Guid id)
        {
            var category = await _categoryUseCase.GetByIdAsync(id);

            return category is not null ?
                   CategoryPresenter.Create(category, _imageUrlResolver) :
                   null;
        }

        public async Task<IEnumerable<CategoryPresenter>> ListAllAsync()
        {
            var categories = await _categoryUseCase.ListAllAsync();

            return categories.Select(category => CategoryPresenter.Create(category, _imageUrlResolver)).ToList();
        }

        public Task<CategoryPresenter?> UpdateAsync(Guid id, CategoryDTO category)
        {
            throw new NotImplementedException();
        }
    }
}
