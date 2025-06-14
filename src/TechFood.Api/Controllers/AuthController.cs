using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Authentication.Commands;

namespace TechFood.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
