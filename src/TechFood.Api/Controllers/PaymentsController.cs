using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Payment.Commands;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class PaymentsController(IMediator useCase) : ControllerBase
{
    private readonly IMediator _useCase = useCase;

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreatePaymentCommand command)
    {
        var result = await _useCase.Send(command);

        return result != null ? Ok(result) : NotFound();
    }

    [HttpPatch("{id:Guid}")]
    public async Task<IActionResult> ConfirmAsync(Guid id)
    {
        await _useCase.Send(new ConfirmPaymentCommand(id));

        return Ok();
    }
}
