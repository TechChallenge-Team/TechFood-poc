using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class PreparationsController(IPreparationUseCase preparationUseCase) : ControllerBase
{
    private readonly IPreparationUseCase _preparationUseCase = preparationUseCase;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _preparationUseCase.GetAllAsync();

        return Ok(result);
    }

    [HttpPatch]
    [Route("{id:guid}/start")]
    public async Task<IActionResult> PrepareAsync(Guid id)
    {
        await _preparationUseCase.StartAsync(id);

        return Ok();
    }

    [HttpPatch]
    [Route("{id:guid}/finish")]
    public async Task<IActionResult> FinishAsync(Guid id)
    {
        await _preparationUseCase.FinishAsync(id);

        return Ok();
    }
}
