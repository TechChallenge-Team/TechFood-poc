using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;

namespace TechFood.Api.Controllers
{
    [ApiController()]
    [Route("v1/category")]
    public class CategoriesController(ICategoryUseCase categoryUseCase) : ControllerBase
    {
        private readonly ICategoryUseCase _categoryUseCase = categoryUseCase;

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var result = await _categoryUseCase.GetCategoriesAsync();

            return Ok(result);
        }

        [HttpGet("{id:string}")]
        public async Task<IActionResult> GetCategoryByIdAsync(Guid id)
        {
            var result = await _categoryUseCase.GetCategoryByIdAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync(CategoryViewModel category)
        {
            var result = await _categoryUseCase.AddCategoryAsync(category);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategoryAsync(Guid id, CategoryViewModel category)
        {
            var result = await _categoryUseCase.UpdateCategoryAsync(id, category);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryAsync(Guid id)
        {
            var result = await _categoryUseCase.DeleteCategoryAsync(id);

            return Ok(result);
        }
    }
}
