using Microsoft.AspNetCore.Mvc;
using TechFoodClean.Application.Controllers;
using TechFoodClean.Application.Interfaces.Controller;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Application.Interfaces.Presenter;

namespace TechFoodClean.Api.Handlers
{
    [ApiController]
    [Route("v1/menu")]
    [Tags("Menu")]
    public class MenuHandler : ControllerBase
    {
        private readonly IMenuController _menuController;
        private readonly IImageUrlResolver _imageUrlResolver;
        public MenuHandler(ICategoryDataSource categoryDataSource,
            IProductDataSource productDataSource,
            IImageDataSource imageDataSource,
            IUnitOfWorkDataSource unitOfWorkDataSource,
            IImageUrlResolver imageUrlResolver
            )
        {
            _imageUrlResolver = imageUrlResolver;
            _menuController = new MenuController(productDataSource,
                categoryDataSource, imageDataSource, unitOfWorkDataSource, _imageUrlResolver);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _menuController.GetAsync();

            return Ok(result);
        }
    }
}
