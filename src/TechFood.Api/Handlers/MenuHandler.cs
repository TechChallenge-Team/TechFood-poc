using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Controllers;
using TechFood.Application.Interfaces.Controller;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Interfaces.Presenter;
using TechFood.Application.Presenters;

namespace TechFood.Api.Handlers
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
        [ProducesResponseType(typeof(MenuPresenter), 200)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _menuController.GetAsync();

            return Ok(result);
        }
    }
}
