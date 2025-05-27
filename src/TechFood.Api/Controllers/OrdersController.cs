using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models.Order;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class OrdersController(IOrderUseCase orderUseCase) : ControllerBase
{
    private readonly IOrderUseCase _orderUseCase = orderUseCase;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderRequest data)
    {
        var result = await _orderUseCase.CreateAsync(data);

        return Ok(result);
    }

    [HttpPatch("{id:Guid}/finish")]
    public async Task<IActionResult> FinishAsync(Guid id)
    {
        var result = await _orderUseCase.FinishAsync(id);

        return result ? Ok() : NotFound();
    }
}
