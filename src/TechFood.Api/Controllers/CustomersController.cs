using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.UseCases.Customer.Commands;
using TechFood.Application.UseCases.Customer.Queries;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class CustomersController(IMediator useCase) : ControllerBase
{
    private readonly IMediator _useCase = useCase;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerCommand command)
    {
        var result = await _useCase.Send(command);

        return Ok(result);
    }

    [HttpGet("{document}")]
    public async Task<IActionResult> GetByDocumentAsync(string document)
    {
        var result = await _useCase.Send(new GetCustomerByDocumentQuery() { DocumentValue = document });

        return result != null ? Ok(result) : NotFound();
    }
}
