using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Category.Commands;
using TechFood.Application.UseCases.Category.Queries;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class CategoriesController(IMediator useCase) : ControllerBase
{
    private readonly IMediator _useCase = useCase;

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _useCase.Send(new GetAllCategoryQuery());

        return Ok(result);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _useCase.Send(new GetCategoryByIdQuery(id));

        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(CreateCategoryCommand command)
    {
        var result = await _useCase.Send(command);

        return Ok(result);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateCategoryCommand command)
    {
        command.Id = id;

        var result = await _useCase.Send(command);

        return Ok(result);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _useCase.Send(new DeleteCategoryCommand(id));

        return NoContent();
    }
}
