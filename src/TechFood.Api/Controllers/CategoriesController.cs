using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers
{
    [ApiController()]
    [Route("v1/categories")]
    public class CategoriesController(ICategoryUseCase categoryUseCase) : ControllerBase
    {
        private readonly ICategoryUseCase _categoryUseCase = categoryUseCase;

        [HttpGet]
        public async Task<IActionResult> ListCategoriesAsync()
        {
            var result = await _categoryUseCase.GetCategoriesAsync();

            return Ok(result);
        }
    }
}
