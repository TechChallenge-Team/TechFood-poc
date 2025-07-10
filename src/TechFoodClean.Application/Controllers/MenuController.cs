using TechFoodClean.Application.Gateway;
using TechFoodClean.Application.Interfaces.Controller;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Application.Interfaces.Gateway;
using TechFoodClean.Application.Interfaces.Presenter;
using TechFoodClean.Application.Presenters;
using TechFoodClean.Application.UseCases;

namespace TechFoodClean.Application.Controllers
{
    public class MenuController : IMenuController
    {
        private readonly IImageUrlResolver _imageUrlResolver;
        private readonly IProductGateway _productGateway;
        private readonly ICategoryGateway _categoryGateway;
        public MenuController(
            IProductDataSource productDataSource,
            ICategoryDataSource categoryDataSource,
            IImageDataSource imageDataSource,
            IUnitOfWorkDataSource unitOfWorkDataSource,
            IImageUrlResolver imageUrlResolver)
        {
            _categoryGateway = new CategoryGateway(categoryDataSource, imageDataSource, unitOfWorkDataSource);
            _productGateway = new ProductGateway(productDataSource, imageDataSource, unitOfWorkDataSource);
            _imageUrlResolver = imageUrlResolver;
        }
        public async Task<MenuPresenter?> GetAsync()
        {
            var _productUseCase = new ProductUseCase(_productGateway);
            var _categoryUseCase = new CategoryUseCase(_categoryGateway);

            var products = await _productUseCase.ListAllAsync();
            var categories = await _categoryUseCase.ListAllAsync();
            if (products is null || categories is null)
            {
                return null;
            }
            return MenuPresenter.Create(products, categories, _imageUrlResolver);
        }
    }
}
