using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Menu.Queries;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class MenuController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _mediator.Send(new GetMenuQuery());

        return Ok(result);
    }
}
