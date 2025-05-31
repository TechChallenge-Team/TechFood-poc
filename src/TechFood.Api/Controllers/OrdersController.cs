using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Order.Commands;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class OrdersController(IMediator userCase) : ControllerBase
{
    private readonly IMediator _userCase = userCase;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderCommand command)
    {
        var result = await _userCase.Send(command);

        return Ok(result);
    }

    [HttpPatch("{id:Guid}/finish")]
    public async Task<IActionResult> FinishAsync(Guid id)
    {
        await _userCase.Send(new FinishOrderCommand(id));

        return Ok();
    }
}
