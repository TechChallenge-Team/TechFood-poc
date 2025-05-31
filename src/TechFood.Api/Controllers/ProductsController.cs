using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Product.Commands;
using TechFood.Application.UseCases.Product.Queries;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class ProductsController(IMediator useCase) : ControllerBase
{
    private readonly IMediator _useCase = useCase;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _useCase.Send(new GetAllProductQuery());

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _useCase.Send(new GetProductByIdQuery(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateProductCommand command)
    {
        var result = await _useCase.Send(command);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateProductCommand command)
    {
        command.Id = id;

        var result = await _useCase.Send(command);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/outOfStock")]
    public async Task<IActionResult> PatchOutOfStockAsync(Guid id, bool request)
    {
        var result = await _useCase.Send(new SetProductOutOfStockCommand { Id = id, OutOfStock = request });

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _useCase.Send(new DeleteProductCommand(id));

        return NoContent();
    }
}
