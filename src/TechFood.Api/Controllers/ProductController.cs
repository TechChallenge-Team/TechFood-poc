using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models.Product;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/products")]
public class ProductController(IProductUseCase categoryUseCase) : ControllerBase
{
    private readonly IProductUseCase _productUseCase = categoryUseCase;

    [HttpGet]
    public async Task<IActionResult> ListAsync()
    {
        var result = await _productUseCase.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _productUseCase.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateProductRequest request)
    {
        var result = await _productUseCase.CreateAsync(request);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateProductRequest request)
    {
        var result = await _productUseCase.UpdateAsync(id, request);

        return result != null ? Ok(result) : NotFound();
    }

    [HttpPatch("{id:guid}/outOfStock")]
    public async Task<IActionResult> PatchOutOfStockAsync(Guid id, bool request)
    {
        var result = await _productUseCase.UpdateOutOfStockAsync(id, request);

        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _productUseCase.DeleteAsync(id);

        return result ? NoContent() : NotFound();
    }
}
