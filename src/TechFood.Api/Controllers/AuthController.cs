using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Authentication.Commands;

namespace TechFood.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AuthController(IMediator useCase) : ControllerBase
    {
        private readonly IMediator _useCase = useCase;

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInCommand command)
        {
            var response = await _useCase.Send(command);
            return Ok(response);
        }
    }
}
