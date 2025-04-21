using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/categories")]
public class ProductController(IProductUseCase categoryUseCase) : ControllerBase
{
    private readonly IProductUseCase _productUseCase = categoryUseCase;

    [HttpGet]
    public async Task<IActionResult> ListProductsAsync()
    {
        var result = await _productUseCase.GetProductsAsync();

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductByIdAsync(Guid id)
    {
        var result = await _productUseCase.GetProductByIdAsync(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(ProductRequestDto request )
    {
        await _productUseCase.CreateProductAsync(request);

        return Created();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProductAsync(Guid id, ProductRequestDto request)
    {
        await _productUseCase.UpdateProductAsync(id, request);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProductAsync(Guid id)
    {
        await _productUseCase.DeleteProductAsync(id);

        return Ok();
    }
}
