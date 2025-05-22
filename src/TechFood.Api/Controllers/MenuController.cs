using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class MenuController(IMenuUseCase menuUseCase) : ControllerBase
{
    private readonly IMenuUseCase _menuUseCase = menuUseCase;

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _menuUseCase.GetAsync();

        return Ok(result);
    }
}
