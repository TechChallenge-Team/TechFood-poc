using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models.Payment;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class PaymentsController(IPaymentUseCase paymentUseCase) : ControllerBase
{
    private readonly IPaymentUseCase _paymentUseCase = paymentUseCase;

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreatePaymentRequest data)
    {
        var result = await _paymentUseCase.CreateAsync(data);

        return result != null ? Ok(result) : NotFound();
    }
}
