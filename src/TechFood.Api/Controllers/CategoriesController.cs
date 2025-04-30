using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models.Category;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;

namespace TechFood.Api.Controllers
{
    [ApiController()]
    [Route("v1/[controller]")]
    public class CategoriesController(ICategoryUseCase categoryUseCase) : ControllerBase
    {
        private readonly ICategoryUseCase _categoryUseCase = categoryUseCase;

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var result = await _categoryUseCase.GetCategoriesAsync();

            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetCategoryByIdAsync(Guid id)
        {
            var result = await _categoryUseCase.GetCategoryByIdAsync(id);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync(CreateCategoryRequest category)
        {
            var result = await _categoryUseCase.AddCategoryAsync(category);

            return Ok(result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateCategoryAsync(Guid id, CreateCategoryRequest category)
        {
            var result = await _categoryUseCase.UpdateCategoryAsync(id, category);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteCategoryAsync(Guid id)
        {
            var result = await _categoryUseCase.DeleteCategoryAsync(id);

            return result ? Ok(result) : NotFound();
        }
    }
}
