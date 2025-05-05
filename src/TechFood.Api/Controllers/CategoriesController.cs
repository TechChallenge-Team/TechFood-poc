using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models.Category;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class CategoriesController(ICategoryUseCase categoryUseCase) : ControllerBase
{
    private readonly ICategoryUseCase _categoryUseCase = categoryUseCase;

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _categoryUseCase.ListAllAsync();

        return Ok(result);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _categoryUseCase.GetByIdAsync(id);

        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(CreateCategoryRequest category)
    {
        var result = await _categoryUseCase.AddAsync(category);

        return Ok(result);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateCategoryRequest category)
    {
        var result = await _categoryUseCase.UpdateAsync(id, category);

        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _categoryUseCase.DeleteAsync(id);

        return result ? NoContent() : NotFound();
    }
}
