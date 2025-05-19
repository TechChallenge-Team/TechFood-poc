using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models.Auth;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUseCase _authUseCase;

        public AuthController(IAuthUseCase authUseCase)
        {
            _authUseCase = authUseCase;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest request)
        {
            var response = await _authUseCase.SignInAsync(request);
            return Ok(response);
        }
    }
}
