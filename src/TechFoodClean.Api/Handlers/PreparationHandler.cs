using Microsoft.AspNetCore.Mvc;
using TechFoodClean.Application.Controllers;
using TechFoodClean.Application.Interfaces.Controller;
using TechFoodClean.Application.Interfaces.DataSource;

namespace TechFoodClean.Api.Handlers;

[ApiController()]
[Route("v1/[controller]")]
[Tags("Preparation")]

public class PreparationHandler : ControllerBase
{
    private readonly IPreparationController _preparationController;
    
    public PreparationHandler(
        IPreparationDataSource preparationDataSource,
        IUnitOfWorkDataSource unitOfWorkDataSource
        )
    {
        _preparationController = new PreparationController(
            preparationDataSource,
            unitOfWorkDataSource);
    }

    [HttpGet]
    [Route("orders")]
    public async Task<IActionResult> GetAllPreparationOrdersAsync()
    {
        return Ok(await _preparationController.GetAllPreparationOrdersAsync());
    }

    [HttpGet]
    [Route("{orderId:guid}/number")]
    public async Task<IActionResult> GetPreparationByOrderIdAsync(Guid orderId)
    {
        return Ok(await _preparationController.GetPreparationByOrderIdAsync(orderId));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _preparationController.GetAllAsync();

        return Ok(result);
    }

    [HttpPatch]
    [Route("{id:guid}/start")]
    public async Task<IActionResult> PrepareAsync(Guid id)
    {
        await _preparationController.StartAsync(id);

        return Ok();
    }

    [HttpPatch]
    [Route("{id:guid}/finish")]
    public async Task<IActionResult> FinishAsync(Guid id)
    {
        await _preparationController.FinishAsync(id);

        return Ok();
    }

    [HttpPatch]
    [Route("{id:guid}/cancel")]
    public async Task<IActionResult> CancelAsync(Guid id)
    {
        await _preparationController.CancelAsync(id);

        return Ok();
    }
}

