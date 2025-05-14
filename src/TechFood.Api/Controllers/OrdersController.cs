using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models.Order;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class OrdersController(IOrderUseCase orderUseCase) : ControllerBase
{
    private readonly IOrderUseCase _orderUseCase = orderUseCase;

    [HttpGet("done-and-preparation")]
    public async Task<IActionResult> GetAllDoneAndInPreparationAsync()
    {
        var result = await _orderUseCase.GetAllDoneAndInPreparationAsync();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderRequest data)
    {
        var result = await _orderUseCase.CreateAsync(data);

        return Ok(result);
    }

    [HttpPatch]
    [Route("{orderId:guid}/prepare")]
    public async Task<IActionResult> PatchPrepareAsync(Guid orderId)
    {
        var result = await _orderUseCase.PrepareAsync(orderId);

        return result ? Ok() : NotFound();
    }

    [HttpPatch]
    [Route("{orderId:guid}/finish")]
    public async Task<IActionResult> PatchFinishAsync(Guid orderId)
    {
        var result = await _orderUseCase.FinishAsync(orderId);

        return result ? Ok() : NotFound();
    }
}
