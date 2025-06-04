using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Preparation.Commands;
using TechFood.Application.UseCases.Preparation.Queries;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class PreparationsController(IMediator useCase) : ControllerBase
{
    private readonly IMediator _useCase = useCase;

    [HttpGet]
    public async Task<IActionResult> GetDailyAsync()
    {
        var result = await _useCase.Send(new GetDailyPreparationsQuery());

        return Ok(result);
    }

    [HttpGet]
    [Route("tracking")]
    public async Task<IActionResult> GetTrackingAsync()
    {
        var result = await _useCase.Send(new GetTrackingPreparationsQuery());

        return Ok(result);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _useCase.Send(new GetPreparationByIdQuery(id));

        return Ok(result);
    }

    [HttpPatch]
    [Route("{id:guid}/start")]
    public async Task<IActionResult> StartAsync(Guid id)
    {
        await _useCase.Send(new StartPreparationCommand(id));

        return Ok();
    }

    [HttpPatch]
    [Route("{id:guid}/complete")]
    public async Task<IActionResult> CompleteAsync(Guid id)
    {
        await _useCase.Send(new CompletePreparationCommand(id));

        return Ok();
    }
}
