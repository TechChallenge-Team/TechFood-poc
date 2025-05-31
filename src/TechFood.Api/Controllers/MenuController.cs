using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Menu.Queries;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class MenuController(IMediator useCase) : ControllerBase
{
    private readonly IMediator _useCase = useCase;

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _useCase.Send(new GetMenuQuery());

        return Ok(result);
    }
}
