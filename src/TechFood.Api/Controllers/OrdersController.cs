using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Order.Commands;
using TechFood.Application.UseCases.Order.Queries;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class OrdersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _mediator.Send(new GetOrdersQuery());

        return Ok(result);
    }

    [HttpGet]
    [Route("ready")]
    public async Task<IActionResult> GetReadyAsync()
    {
        var result = await _mediator.Send(new GetReadyOrdersQuery());

        return Ok(result);
    }

    [HttpPatch]
    [Route("{id:guid}/deliver")]
    public async Task<IActionResult> DeliverAsync(Guid id)
    {
        await _mediator.Send(new DeliverOrderCommand(id));

        return Ok();
    }
}
