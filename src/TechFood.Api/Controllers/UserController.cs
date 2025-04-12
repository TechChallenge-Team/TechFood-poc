using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers
{
    [ApiController()]
    [Route("v1/user")]
    public class UserController(IUserUseCase userUseCase) : ControllerBase
    {
        private readonly IUserUseCase _userUseCase = userUseCase;

        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync()
        {
            var result = await _userUseCase.GetUserByIdAsync(User.GetUserId());

            return Ok(result);
        }
    }
}
