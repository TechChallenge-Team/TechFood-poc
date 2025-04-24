using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models.Customer;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers
{
    [ApiController()]
    [Route("v1/[controller]")]
    public class CustomerController(ICustomerUseCase customerUseCase) : ControllerBase
    {
        private readonly ICustomerUseCase _customerUseCase = customerUseCase;
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerRequest data)
        {
            var result = await _customerUseCase.AddItemAsync(data);


            return Ok(result);
        }
    }
}
