using Microsoft.AspNetCore.Mvc;
using TechFoodClean.Application.Controllers;
using TechFoodClean.Application.Interfaces.Controller;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Application.Interfaces.Presenter;
using TechFoodClean.Common.Category;

namespace TechFoodClean.Api.Handlers
{
    [ApiController()]
    [Route("v1/categories")]
    public class CategoriesHandler : ControllerBase
    {
        private readonly ICategoryController _categoryController;

        public CategoriesHandler(ICategoryDataSource _categoryDataSource,
                                 IImageUrlResolver imageUrlResolver,
                                 IUnitOfWorkDataSource unitOfWorkDataSource,
                                 IImageDataSource imageDataSource)
        {
            _categoryController = new CategoryController(_categoryDataSource,
                                                         imageUrlResolver,
                                                         imageDataSource,
                                                         unitOfWorkDataSource);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _categoryController.ListAllAsync();

            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _categoryController.GetByIdAsync(id);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateCategoryRequestDTO category)
        {
            var result = await _categoryController.AddAsync(category);

            return Ok(result);
        }

        //[HttpPut("{id:Guid}")]
        //public async Task<IActionResult> UpdateAsync(Guid id, UpdateCategoryRequest category)
        //{
        //    var result = await _categoryUseCase.UpdateAsync(id, category);

        //    return result != null ? Ok(result) : NotFound();
        //}

        //[HttpDelete("{id:Guid}")]
        //public async Task<IActionResult> DeleteAsync(Guid id)
        //{
        //    var result = await _categoryUseCase.DeleteAsync(id);

        //    return result ? NoContent() : NotFound();
        //}
    }
}
