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

    [HttpPost]
    [Route("{orderId:guid}/items")]
    public async Task<IActionResult> AddItemAsync(Guid orderId, AddOrderItemRequest data)
    {
        var result = await _orderUseCase.AddItemAsync(orderId, data);

        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete]
    [Route("{orderId:guid}/items/{itemId:guid}")]
    public async Task<IActionResult> RemoveItemAsync(Guid orderId, Guid itemId)
    {
        var result = await _orderUseCase.RemoveItemAsync(orderId, itemId);

        return result ? Ok() : NotFound();
    }

    [HttpPost]
    [Route("{orderId:guid}/payment")]
    public async Task<IActionResult> CreatePaymentAsync(Guid orderId, CreatePaymentRequest data)
    {
        var result = await _orderUseCase.CreatePaymentAsync(orderId, data);

        return result != null ? Ok(result) : NotFound();
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
