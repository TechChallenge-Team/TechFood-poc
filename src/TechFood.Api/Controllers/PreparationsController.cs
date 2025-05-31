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

    //[HttpGet]
    //[Route("orders")]
    //public async Task<IActionResult> GetAllPreparationOrdersAsync()
    //{
    //    return Ok(await _preparationUseCase.GetAllPreparationOrdersAsync());
    //}

    //[HttpGet]
    //[Route("{orderId:guid}/number")]
    //public async Task<IActionResult> GetPreparationByOrderIdAsync(Guid orderId)
    //{
    //    return Ok(await _preparationUseCase.GetPreparationByOrderIdAsync(orderId));
    //}

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _useCase.Send(new GetMonitorPreparationsQuery());

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
    [Route("{id:guid}/finish")]
    public async Task<IActionResult> FinishAsync(Guid id)
    {
        await _useCase.Send(new FinishPreparationCommand(id));

        return Ok();
    }

    //[HttpPatch]
    //[Route("{id:guid}/cancel")]
    //public async Task<IActionResult> CancelAsync(Guid id)
    //{
    //    await _preparationUseCase.CancelAsync(id);

    //    return Ok();
    //}
}
